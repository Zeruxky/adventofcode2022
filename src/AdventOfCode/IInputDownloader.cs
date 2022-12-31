namespace AdventOfCode
{
    public interface IInputDownloader
    {
        public Task<Stream> GetAsStreamAsync(Day day, CancellationToken ct);
    }
}