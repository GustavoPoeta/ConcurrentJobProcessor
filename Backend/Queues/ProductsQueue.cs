using ConcurrentJobProcessor.Models;
using System.Collections.Concurrent;

namespace ConcurrentJobProcessor.Queues
{
    public class ProductsQueue
    {
        private readonly ConcurrentQueue<Job> _queue = new();

        public void Enqueue(Job job)
        {
            _queue.Enqueue(job);
        }

        public bool TryDequeue(out Job? job)
        {
            return _queue.TryDequeue(out job);
        }

        public int Count => _queue.Count;
    }
}
