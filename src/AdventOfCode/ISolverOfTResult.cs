namespace AdventOfCode
{
    public interface ISolver<TResult> : ISolver
    {
        public Task<TResult> SolveAsync(Stream stream, CancellationToken ct);
    }
}