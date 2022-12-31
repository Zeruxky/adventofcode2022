namespace AdventOfCode.Day7
{
    /// <summary>
    /// Represents a file within the elves <see cref="FileSystem"/>.
    /// </summary>
    public record File
    {
        /// <summary>
        /// Gets the name of the <see cref="File"/>.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets the size of the <see cref="File"/>.
        /// </summary>
        public int Size { get; init; }

        /// <summary>
        /// Tries to parse the provided <paramref name="value"/> into a <see cref="File"/>.
        /// </summary>
        /// <param name="value">The string to parse.</param>
        /// <param name="file">When this method returns, contains the result of successfully parsing
        /// <paramref name="value"/> or an undefined value on failure.</param>
        /// <returns><c>true</c> if <paramref name="value"/> was successfully parsed; otherwise <c>false</c>.</returns>
        public static bool TryParse(string value, out File? file)
        {
            var separatorIndex = value.IndexOf(' ');
            if (separatorIndex <= 0)
            {
                file = default;
                return false;
            }

            if (!int.TryParse(value.AsSpan(0, separatorIndex), out var size))
            {
                file = default;
                return false;
            }
            
            file = new File()
            {
                Size = size,
                Name = value[(separatorIndex + 1)..],
            };
            return true;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.Name} ({this.Size})";
        }
    }
}