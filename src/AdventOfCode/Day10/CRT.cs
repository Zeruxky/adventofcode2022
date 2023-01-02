using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using Microsoft.Extensions.Primitives;

namespace AdventOfCode.Day10
{
    public record CRT
    {
        private readonly Sprite sprite;
        private readonly StringBuilder display;
        private int position;

        public CRT()
        {
            this.sprite = new Sprite();
            this.display = new StringBuilder();
        }

        public IEnumerable<string> Draw(IEnumerable<RegisterMeasurement> registerMeasurements)
        {
            foreach (var registerMeasurement in registerMeasurements)
            {
                sprite.MoveTo(registerMeasurement.Value);
                var characterToWrite = sprite.Pixels.Contains(position)
                    ? '#'
                    : '.';

                this.display.Append(characterToWrite);
                this.position++;
                if (this.position == 40)
                {
                    var displayLine = this.display.ToString();
                    yield return displayLine;
                    this.display.Clear();
                    this.position = 0;
                }
            }
        }
    }
}