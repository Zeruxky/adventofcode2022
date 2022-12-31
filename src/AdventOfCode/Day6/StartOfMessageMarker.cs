namespace AdventOfCode.Day6
{
    public class StartOfMessageMarker : MarkerDetectorBase
    {
        public StartOfMessageMarker(Stream stream, bool leaveOpen)
            : base(14, stream, leaveOpen)
        {
        }
    }
}