using ConcurrentJobProcessor.Models;
using System.Threading.Channels;

namespace ConcurrentJobProcessor.Channels
{
    public class ProductsChannel
    {
        private readonly Channel<Job> _channel;

        public ProductsChannel()
        {
            _channel = Channel.CreateUnbounded<Job>();
        }

        public async ValueTask WriteAsync(Job job)
        {
            await _channel.Writer.WriteAsync(job);
        }

        public async ValueTask<Job> ReadAsync(CancellationToken cancellationToken)
        {
            return await _channel.Reader.ReadAsync(cancellationToken);
        }
    }
}
