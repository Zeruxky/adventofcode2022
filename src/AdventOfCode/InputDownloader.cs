namespace AdventOfCode
{
    public class InputDownloader : IInputDownloader
    {
        private readonly HttpClient client;

        public InputDownloader(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Stream> GetAsStreamAsync(Day day, CancellationToken ct)
        {
            var content = await this.client
                .GetStreamAsync($"day/{day.Value}/input", ct)
                .ConfigureAwait(false);
            return content;
        }
    }
}