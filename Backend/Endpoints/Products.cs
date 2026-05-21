using ConcurrentJobProcessor.Endpoints.Contracts;
using ConcurrentJobProcessor.Models;
using ConcurrentJobProcessor.Queues;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace ConcurrentJobProcessor.Endpoints
{
    public class Products : EndpointGroupBase
    {
        private readonly ProductsQueue _productsQueue;

        public Products (ProductsQueue productsQueue)
        {
            _productsQueue = productsQueue;
        }

        public override string? GroupName => "products";

        public override void Map(RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/upload-csv", AddProductsCsv).DisableAntiforgery() ;
        }

        public async Task<Ok> AddProductsCsv([FromForm] AddProductsCsvRequest request)
        {
            using MemoryStream memoryStream = new();

            await request.File.CopyToAsync(memoryStream);

            Job job = new(Guid.NewGuid(), "addProducts", memoryStream.ToArray());

            _productsQueue.Enqueue(job);

            return TypedResults.Ok();
        }
    }
}
