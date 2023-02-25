namespace Logic.Day8
{
    public static class ForestCreator
    {
        public static Tree[,] CreateForest(List<List<int>> forest)
        {
            var back = new Tree[forest[0].Count, forest.Count];

            for (int y = 0; y < forest.Count; y++)
            {
                var row = forest[y];
                for (int x = 0; x < row.Count; x++)
                {
                    back[x, y] = new Tree() { Value = row[x] };
                }
            }

            for (int y = 0; y < forest.Count; y++)
            {
                var row = forest[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var n = back[x,y].Neighbours;
                    if (x > 0)
                        n.Add(Direction.Left, back[x-1, y]);

                    if (y>0)
                        n.Add(Direction.Up, back[x, y-1]);

                    if (x < row.Count-1)
                        n.Add(Direction.Right, back[x+1, y]);

                    if (y < forest.Count-1)
                        n.Add(Direction.Down, back[x, y+1]);
                }
            }

            return back;
        }
    }
}
