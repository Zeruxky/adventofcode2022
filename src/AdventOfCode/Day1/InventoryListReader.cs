using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day1
{
    public sealed class InventoryListReader : IDisposable
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly StreamReader reader;

        private bool disposed = false;

        public InventoryListReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, encoding, leaveOpen: leaveOpen);
        }

        public async IAsyncEnumerable<CalorieItemInventory> ReadAllAsync([EnumeratorCancellation] CancellationToken ct)
        {
            var inventory = new CalorieItemInventory();
            while (!ct.IsCancellationRequested)
            {
                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    // Check if there is something to yield back.
                    if (inventory.Items.Any())
                    {
                        yield return inventory;
                    }
                    
                    yield break;
                }
                
                if (InventoryListReader.IsSeparatorLine(line))
                {
                    yield return inventory;
                    inventory = new CalorieItemInventory();
                }
            
                if (CalorieItem.TryParse(line.AsSpan(), out var calories))
                {
                    inventory.Add(calories);
                }
            }
        
            ct.ThrowIfCancellationRequested();
        }
    
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
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
    
        internal static bool IsSeparatorLine(string? line)
        {
            if (line == string.Empty)
            {
                return true;
            }

            if (line == " ")
            {
                return true;
            }

            if (line == Environment.NewLine)
            {
                return true;
            }

            return false;
        }
    }
}