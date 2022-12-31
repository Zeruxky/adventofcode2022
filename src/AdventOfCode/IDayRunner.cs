namespace AdventOfCode
{
    public interface IDayRunner
    {
        public Day Day { get; }
        
        public Task RunAsync(CancellationToken ct);
    }
}