using System.Text;

namespace AdventOfCode.Day7
{
    public class TerminalOutputReader : IDisposable
    {
        private readonly FileSystem fileSystem;
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;
        private bool disposed = false;

        public TerminalOutputReader(Stream stream, bool leaveOpen = false)
        {
            this.fileSystem = new FileSystem();
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async Task<FileSystem> ReadAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (this.reader.EndOfStream)
                {
                    break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    break;
                }

                if (!UserCommand.TryParse(line, out var command))
                {
                    continue;
                }

                if (command!.Type == UserCommandType.ChangeDirectory)
                {
                    this.fileSystem.ChangeDirectory(command.Parameters);
                }

                if (command!.Type == UserCommandType.ListItems)
                {
                    await ProcessItemsAsync(ct).ConfigureAwait(false);
                }
            }
            
            ct.ThrowIfCancellationRequested();
            return fileSystem;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.reader.Dispose();
            }

            this.disposed = true;
        }
        
        private async Task ProcessItemsAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                if (this.reader.EndOfStream)
                {
                    break;
                }
                
                var nextCharacter = (char)reader.Peek();
                if (nextCharacter == '$')
                {
                    break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    break;
                }

                if (line.StartsWith("dir"))
                {
                    var index = line.IndexOf(" ", StringComparison.OrdinalIgnoreCase);
                    var directoryName = line[(index + 1)..];
                    this.fileSystem.CreateDirectory(directoryName);
                }

                if (File.TryParse(line, out var file))
                {
                    this.fileSystem.Add(file!);
                }
            }
            
            ct.ThrowIfCancellationRequested();
        }
    }
}