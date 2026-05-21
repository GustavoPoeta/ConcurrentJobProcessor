namespace ConcurrentJobProcessor.Endpoints.Contracts
{
    public sealed class AddProductsCsvRequest
    {
        public IFormFile File { get; set; } = default!;
    }
}
