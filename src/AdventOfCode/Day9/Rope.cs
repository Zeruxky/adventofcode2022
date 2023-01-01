namespace AdventOfCode.Day9
{
    public class Rope
    {
        private readonly Knot[] knots;

        public Rope(IEnumerable<Knot> knots)
        {
            this.knots = knots.ToArray();
            this.Head = this.knots.First();
            this.Tail = this.knots.Last();
        }

        public Knot Head { get; }

        public Knot Tail { get; }

        public static Rope Create(int numberOfKnots = 1)
        {
            var knots = Enumerable.Range(1, numberOfKnots)
                .Select(i => new Knot()
                {
                    Index = i,
                });

            var rope = new Rope(knots);
            return rope;
        }

        public void Move(Direction direction)
        {
            this.Head.Move(direction);
            var leader = this.Head;
            for (var i = 1; i < this.knots.Length; i++)
            {
                var follower = this.knots[i];
                var route = follower.Position.CalculateRoute(leader.Position);
                foreach (var routeDirection in route)
                {
                    follower.Move(routeDirection);
                }

                leader = follower;
            }
        }
    }
}