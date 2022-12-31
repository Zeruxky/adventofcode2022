namespace AdventOfCode.Day6
{
    public class StartOfPacketMarker : MarkerDetectorBase
    {
        public StartOfPacketMarker(Stream stream, bool leaveOpen)
            : base(4, stream, leaveOpen)
        {
        }
    }
}