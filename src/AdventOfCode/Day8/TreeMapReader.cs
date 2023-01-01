using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode.Day8
{
    public class TreeMapReader : IDisposable
    {
        private readonly StreamReader reader;
        private readonly Encoding encoding = Encoding.UTF8;
        private bool disposed = false;

        public TreeMapReader(Stream stream, bool leaveOpen = false)
        {
            this.reader = new StreamReader(stream, this.encoding, leaveOpen: leaveOpen);
        }

        public async Task<TreeMap> ReadAsync(CancellationToken ct)
        {
            var trees = new List<Tree>();
            var positionY = 1;
            while (true)
            {
                ct.ThrowIfCancellationRequested();
                if (this.reader.EndOfStream)
                {
                    break;
                }

                var line = await this.reader.ReadLineAsync().ConfigureAwait(false);
                if (line is null)
                {
                    break;
                }

                trees.AddRange(ParseTrees(line, positionY));
                positionY++;
            }
            
            var map = new TreeMap(trees);
            return map;
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
        
        private static IEnumerable<Tree> ParseTrees(string line, int positionY)
        {
            var positionX = 1;
            foreach (var size in line.Select(character => int.Parse(character.ToString())))
            {
                var tree = new Tree()
                {
                    Height = size,
                    Coordinate = new MapCoordinate()
                    {
                        X = positionX++,
                        Y = positionY,
                    },
                };

                yield return tree;
            }
        }
    }
}