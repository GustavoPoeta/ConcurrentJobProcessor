using ConcurrentJobProcessor.Queues;

namespace ConcurrentJobProcessor.Workers
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                using IServiceScope scope = _serviceProvider.CreateScope();
                var workers = scope.ServiceProvider.GetServices<IWorker>();

                foreach (var worker in workers)
                {
                    await worker.ExecuteAsync(cancellationToken);
                }
            }
        }
    }
}
