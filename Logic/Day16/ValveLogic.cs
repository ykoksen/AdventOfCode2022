//namespace Logic.Day16
//{
//    public class ValveReader
//    {

//        public static (Valve valve, string[] tunnels) ReadLine(string input)
//        {
//            var split = input.Split(";");
//            var valve = new Valve(input[6..8], int.Parse(split[0][23..]));

//            string[] tunnels;
//            if (split[1].Contains("tunnels"))
//            {
//                tunnels = split[1][24..].Split(",").Select(x => x.Trim()).ToArray();
//            }
//            else
//            {
//                tunnels = new[] { split[1][23..] };
//            }

//            return (valve, tunnels);
//        }

//        public static async Task<Dictionary<string, Valve>> ReadTextAsync(StreamReader reader)
//        {
//            var valves = new Dictionary<string, Valve>();
//            var listFuncs = new List<Action>();

//            while (!reader.EndOfStream)
//            {
//                var (valve, tunnels) = ReadLine((await reader.ReadLineAsync())!);
//                valves.Add(valve.Name, valve);

//                listFuncs.Add(() => Array.ForEach(tunnels, x => valve.Tunnels.Add(valves[x])));
//            }

//            listFuncs.ForEach(x => x());

//            return valves;
//        }

//    }

//    public struct Valve : IEquatable<Valve>
//    {
//        public string Name { get; }
//        public int Flow { get; }

//        public HashSet<Valve> Tunnels { get; }

//        public Valve(string name, int flow)
//        {
//            Name = name;
//            Flow = flow;
//            Tunnels = new HashSet<Valve>();
//        }

//        public bool Equals(Valve? other)
//        {
//            return Name == other?.Name;
//        }

//        public override bool Equals(object? obj)
//        {
//            return Equals(obj as Valve);
//        }

//        public override int GetHashCode()
//        {
//            return Name.GetHashCode();
//        }

//        public override string ToString()
//        {
//            return $"({Name}: {Flow})";
//        }
//    }
//}