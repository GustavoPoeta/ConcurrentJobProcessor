namespace ConcurrentJobProcessor.Workers
{
    public interface IWorker
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
