using ConcurrentJobProcessor.Helpers;
using ConcurrentJobProcessor.Hubs;
using ConcurrentJobProcessor.Models;
using ConcurrentJobProcessor.Queues;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ConcurrentJobProcessor.Workers
{
    public class AddProductsWorker : IWorker
    {
        private readonly ProductsQueue _queue;
        private readonly ProductsHub _hub;
        private readonly AppDbContext _appDbContext;

        public AddProductsWorker(ProductsQueue queue, ProductsHub hub, AppDbContext appDbContext) 
        {
            _queue = queue;
            _hub = hub;
            _appDbContext = appDbContext;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var job)) {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        PrepareHeaderForMatch = args => args.Header.ToLower(),
                    };

                    try
                    {
                        using MemoryStream memoryStream = new(job!.File);


                        using (var reader = new StreamReader(memoryStream))
                        using (var csv = new CsvReader(reader, config))
                        {
                            csv.Context.RegisterClassMap<ProductsMap>();

                            var records = csv.GetRecords<Products>().ToList();

                            if (records.Count == 0)
                            {
                                throw new BadHttpRequestException("No products to be found in request.");
                            }

                            await _appDbContext.Products.AddRangeAsync(records, cancellationToken);
                            await _appDbContext.SaveChangesAsync(cancellationToken);

                            var updatedProducts = await _appDbContext.Products.ToListAsync(cancellationToken);

                            await _hub.SendProductsUpdatedOnAdd(updatedProducts);
                        }
                    }
                    catch (Exception) 
                    {
                        await _hub.SendProductsUpdatedOnAdd(new List<Products>());
                    }
                }
            }
        }
    }
}
