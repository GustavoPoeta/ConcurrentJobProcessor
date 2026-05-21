namespace ConcurrentJobProcessor.Models
{
    public record Job(
        Guid Id,
        string Type,
        byte[] File
        );        
}
