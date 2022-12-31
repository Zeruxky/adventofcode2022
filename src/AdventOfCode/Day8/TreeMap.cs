namespace AdventOfCode.Day8
{
    public class TreeMap
    {
        private readonly List<Tree> trees;
        
        public TreeMap()
            : this(Enumerable.Empty<Tree>())
        {
        }

        public TreeMap(IEnumerable<Tree> trees)
        {
            this.trees = trees.ToList();
            this.Columns = this.trees.Max(t => t.Coordinate.X);
            this.Rows = this.trees.Max(t => t.Coordinate.Y);
        }

        public int Columns { get; }

        public int Rows { get; }

        public IEnumerable<int> GetScenicScores()
        {
            foreach (var tree in this.trees)
            {
                if (tree.Coordinate.Y == 1)
                {
                    yield return 0;
                    continue;
                }

                if (tree.Coordinate.Y > 1 && tree.Coordinate.Y < this.Rows)
                {
                    if (tree.Coordinate.X == 1)
                    {
                        yield return 0;
                        continue;
                    }
                    
                    if (tree.Coordinate.X == this.Columns)
                    {
                        yield return 0;
                        continue;
                    }

                    var scenicScore = this.GetScenicScore(tree);
                    yield return scenicScore;
                }

                if (tree.Coordinate.Y == this.Rows)
                {
                    yield return 0;
                }
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
            var smallerTrees = this.GetTrees(tree, direction).TakeWhile(t => t.Height < tree.Height);
            return smallerTrees;
        }

        public IEnumerable<Tree> GetVisibleTrees()
        {
            foreach (var tree in this.trees)
            {
                if (tree.Coordinate.Y == 1)
                {
                    yield return tree;
                    continue;
                }
                
                if (tree.Coordinate.Y > 1 && tree.Coordinate.Y < this.Rows)
                {
                    if (tree.Coordinate.X == 1)
                    {
                        yield return tree;
                        continue;
                    }
                    
                    if (tree.Coordinate.X == this.Columns)
                    {
                        yield return tree;
                        continue;
                    }

                    if (this.IsTallest(tree))
                    {
                        yield return tree;
                        continue;
                    }
                }

                // Bottom row
                if (tree.Coordinate.Y == this.Rows)
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

        private IEnumerable<Tree> GetTrees(Tree tree, Direction direction)
        {
            if (direction == Direction.North)
            {
                return this.trees
                    .Where(t => t.Coordinate.X == tree.Coordinate.X)
                    .Where(t => t.Coordinate.Y < tree.Coordinate.Y)
                    .OrderByDescending(t => t.Coordinate.Y);
            }
            
            if (direction == Direction.East)
            {
                return this.trees
                    .Where(t => t.Coordinate.Y == tree.Coordinate.Y)
                    .Where(t => t.Coordinate.X > tree.Coordinate.X)
                    .OrderBy(t => t.Coordinate.X);
            }
            
            if (direction == Direction.South)
            {
                return this.trees
                    .Where(t => t.Coordinate.X == tree.Coordinate.X)
                    .Where(t => t.Coordinate.Y > tree.Coordinate.Y)
                    .OrderBy(t => t.Coordinate.Y);
            }
            
            if (direction == Direction.West)
            {
                return this.trees
                    .Where(t => t.Coordinate.Y == tree.Coordinate.Y)
                    .Where(t => t.Coordinate.X < tree.Coordinate.X)
                    .OrderByDescending(t => t.Coordinate.X);
            }
            
            return Enumerable.Empty<Tree>();
        }
    }
}