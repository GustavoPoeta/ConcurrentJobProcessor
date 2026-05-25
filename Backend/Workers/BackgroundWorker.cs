using ConcurrentJobProcessor.Channels;
using ConcurrentJobProcessor.Helpers;
using ConcurrentJobProcessor.Hubs;
using ConcurrentJobProcessor.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Threading.Channels;

namespace ConcurrentJobProcessor.Workers
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProductsChannel _channel;

        public BackgroundWorker(IServiceProvider serviceProvider, ProductsChannel channel)
        {
            _serviceProvider = serviceProvider;
            _channel = channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await _channel.ReadAsync(stoppingToken);

                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var db = scope.ServiceProvider
                        .GetRequiredService<AppDbContext>();

                    var hub = scope.ServiceProvider
                        .GetRequiredService<IHubContext<ProductsHub>>();

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                    };

                    using var memoryStream = new MemoryStream(job.File);

                    using var reader = new StreamReader(memoryStream);

                    using var csv = new CsvReader(reader, config);

                    csv.Context.RegisterClassMap<ProductsMap>();

                    var records = csv.GetRecords<Products>().ToList();

                    if (records.Count == 0)
                    {
                        throw new BadHttpRequestException(
                            "No products found.");
                    }

                    await db.Products.AddRangeAsync(records, stoppingToken);

                    await db.SaveChangesAsync(stoppingToken);

                    var updatedProducts = await db.Products
                        .ToListAsync(cancellationToken: stoppingToken);

                    await hub.Clients.All.SendAsync(
                        "ProductsUpdated",
                        updatedProducts,
                        cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    using var scope = _serviceProvider.CreateScope();

                    var hub = scope.ServiceProvider
                        .GetRequiredService<IHubContext<ProductsHub>>();

                    await hub.Clients.All.SendAsync(
                        "ProductsError",
                        ex.Message,
                        cancellationToken: stoppingToken);
                }
            }
        }
    }
}
