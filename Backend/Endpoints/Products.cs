using ConcurrentJobProcessor.Channels;
using ConcurrentJobProcessor.Endpoints.Contracts;
using ConcurrentJobProcessor.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;


namespace ConcurrentJobProcessor.Endpoints
{
    public class Products : EndpointGroupBase
    {
        private readonly ProductsChannel _channel;

        public Products (ProductsChannel channel)
        {
            _channel = channel;
        }

        public override string? GroupName => "products";

        public override void Map(RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/upload-csv", AddProductsCsv).DisableAntiforgery() ;
        }

        public async Task<Ok> AddProductsCsv([FromForm] AddProductsCsvRequest request)
        {
            using var memoryStream = new MemoryStream();

            await request.File.CopyToAsync(memoryStream);

            await _channel.WriteAsync(new Job(Guid.NewGuid(), "Prod", memoryStream.ToArray()));

            return TypedResults.Ok();
        }
    }
}
