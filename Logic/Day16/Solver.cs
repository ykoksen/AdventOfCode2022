//namespace Logic.Day16
//{
//    public static class Solver
//    {
//        public static async Task<string> Solve()
//        {
//            await using var stream = InputLoader.Load(16);
//            using var reader = new StreamReader(stream);

//            var valves = ValveReader.ReadTextAsync(reader);

//            return "42";
//        }
//    }

//    public class Solve
//    {
//        public int HighestValue { get; set; } = 0;

//        private Solve()
//        {}

//        public static int Solv(Valve startValve)
//        {
//            var solver = new Solve();
//            solver.SolveFor(startValve);
//            return solver.HighestValue;
//        }

//        private void SolveFor(Valve startValve)
//        {
//            Queue<TreeNode> queue = new Queue<TreeNode>();


//        }
//    }

//    public class TreeNode
//    {


//        public Valve Node { get; }

//        public TreeNode? Parent { get; }

//        private TreeNode(Valve node) 
//        {
//            Node = node;
//        }

//        private TreeNode(Valve node, TreeNode parent) : this(node)
//        {
//            Parent = parent;            
//        }

//        public static TreeNode CreateRoot(Valve node)
//        {
//            return new TreeNode(node);
//        }

//        public TreeNode CreateChild(Valve node)
//        {
//            return new TreeNode(node, this);
//        }

//        public bool Exists(Valve node)
//        {
//            return true;
//            // return Node == node || (Parent?.Exists(node) ?? false);
//        }
//    }
//}