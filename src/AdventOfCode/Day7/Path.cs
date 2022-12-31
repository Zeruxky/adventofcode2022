namespace AdventOfCode.Day7
{
    /// <summary>
    /// Provides functionality for path operations.
    /// </summary>
    public static class Path
    {
        /// <summary>
        /// Combines the provided <paramref name="path"/> and <paramref name="otherPath"/>.
        /// </summary>
        /// <param name="path">The first path to combine.</param>
        /// <param name="otherPath">The second path to combine.</param>
        /// <returns>The combined path of the provided <paramref name="path"/> and <paramref name="otherPath"/>
        /// as a string.</returns>
        /// <remarks>
        /// If the <paramref name="path"/> is the <see cref="Directory.RootDirectoryPath"/>, then the
        /// <paramref name="otherPath"/> will be returned.
        /// </remarks>
        public static string Combine(string path, string otherPath)
        {
            if (path == Directory.RootDirectoryPath)
            {
                return otherPath;
            }
            
            return $"{path}/{otherPath}";
        }
    }
}