namespace AdventOfCode.Day8
{
    public class TreeMap
    {
        private readonly List<Tree> trees;

        public TreeMap(IEnumerable<Tree> trees)
        {
            this.trees = trees.ToList();
            this.Size = new MapSize()
            {
                Columns = this.trees.Max(t => t.Coordinate.X),
                Rows = this.trees.Max(t => t.Coordinate.Y),
            };
        }

        public MapSize Size { get; }

        public IEnumerable<int> GetScenicScores()
        {
            foreach (var tree in this.trees)
            {
                if (tree.IsOnEdge(this.Size))
                {
                    yield return 0;
                    continue;
                }

                var scenicScore = this.GetScenicScore(tree);
                yield return scenicScore;
            }
        }

        private int GetScenicScore(Tree tree)
        {
            var scenicScore = 1;
            scenicScore *= this.GetViewingDistance(tree, Direction.North);
            scenicScore *= this.GetViewingDistance(tree, Direction.East);
            scenicScore *= this.GetViewingDistance(tree, Direction.South);
            scenicScore *= this.GetViewingDistance(tree, Direction.West);
            return scenicScore;
        }

        private int GetViewingDistance(Tree tree, Direction direction)
        {
            var viewingDistance = 0;
            if (this.IsTallest(tree, direction))
            {
                viewingDistance = this.GetTrees(tree, direction).Count();
                return viewingDistance;
            }
            
            viewingDistance += this.GetSmallerTrees(tree, direction).Count() + 1;
            return viewingDistance;
        }

        private IEnumerable<Tree> GetSmallerTrees(Tree tree, Direction direction)
        {
            var smallerTrees = this.GetTrees(tree, direction)
                .TakeWhile(t => t.Height < tree.Height);
            
            return smallerTrees;
        }

        public IEnumerable<Tree> GetVisibleTrees()
        {
            foreach (var tree in this.trees)
            {
                if (tree.IsOnEdge(this.Size))
                {
                    yield return tree;
                    continue;
                }
                
                if (this.IsTallest(tree))
                {
                    yield return tree;
                }
            }
        }

        private bool IsTallest(Tree tree)
        {
            if (this.IsTallest(tree, Direction.North))
            {
                return true;
            }
                    
            if (this.IsTallest(tree, Direction.East))
            {
                return true;
            }

            if (this.IsTallest(tree, Direction.South))
            {
                return true;
            }

            if (this.IsTallest(tree, Direction.West))
            {
                return true;
            }

            return false;
        }

        private bool IsTallest(Tree tree, Direction direction)
        {
            return this.GetTrees(tree, direction).All(t => t.Height < tree.Height);
        }

        private IEnumerable<Tree> GetTrees(Tree tree, Direction lookingDirection)
        {
            var treesInLookingDirection = Enumerable.Empty<Tree>();
            if (lookingDirection == Direction.North)
            {
                treesInLookingDirection = this.trees
                    .Where(t => t.Coordinate.X == tree.Coordinate.X)
                    .Where(t => t.Coordinate.Y < tree.Coordinate.Y)
                    .OrderByDescending(t => t.Coordinate.Y);
            }
            
            if (lookingDirection == Direction.East)
            {
                treesInLookingDirection = this.trees
                    .Where(t => t.Coordinate.Y == tree.Coordinate.Y)
                    .Where(t => t.Coordinate.X > tree.Coordinate.X)
                    .OrderBy(t => t.Coordinate.X);
            }
            
            if (lookingDirection == Direction.South)
            {
                treesInLookingDirection = this.trees
                    .Where(t => t.Coordinate.X == tree.Coordinate.X)
                    .Where(t => t.Coordinate.Y > tree.Coordinate.Y)
                    .OrderBy(t => t.Coordinate.Y);
            }
            
            if (lookingDirection == Direction.West)
            {
                treesInLookingDirection = this.trees
                    .Where(t => t.Coordinate.Y == tree.Coordinate.Y)
                    .Where(t => t.Coordinate.X < tree.Coordinate.X)
                    .OrderByDescending(t => t.Coordinate.X);
            }

            return treesInLookingDirection;
        }
    }
}