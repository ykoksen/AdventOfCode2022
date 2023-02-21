using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day18
{
    public static class Solver
    {
        public static async Task<string> Solve(bool removeInternal)
        {
            var input = await Reader.ReadCubes();

            HashSetCount sides = new HashSetCount();
            
            foreach (var cube in input)
            {
                foreach (var side in cube.GetSides())
                {
                    sides.Add(side);
                }                
            }

            foreach(var cube in input)
            {
                sides.Remove(cube);
            }

            if (removeInternal)
            {
                sides = RemoveInternalSides(sides, input.ToHashSet());
            }

            return sides.Count.ToString();
        }

        private static HashSetCount RemoveInternalSides(HashSetCount sides, HashSet<Cube> inputCubes)
        {
            HashSet<Cube> sidesToKeep = new HashSet<Cube>();

            // Since it is lowest it must be outside
            var lowestSide = FindLowest(sides.Cubes);

            HashSetCount sidesToIterate = new HashSetCount();
            sidesToKeep.Add(lowestSide);

            foreach (var cube in lowestSide.GetAllPossibleAdjant())
            {
                sidesToIterate.Add(cube);
            }

            // traverse around the structure
            while(sidesToIterate.Count > 0)
            {
                var cubeCount = sidesToIterate.First();
                sidesToIterate.Remove(cubeCount.cube);
                
                if (sides.Contains(cubeCount.cube) && sidesToKeep.Add(cubeCount.cube))
                {                 
                    foreach (var side in cubeCount.cube.GetSides().Where(side => !inputCubes.Contains(side)))
                    {
                        sidesToIterate.SetDirect(side, 2);
                    }
                }
                else 
                {
                    cubeCount.counter.Value--;

                    if (cubeCount.counter.Value > 0)
                    {
                        foreach (var side in cubeCount.cube.GetSides().Where(side => !inputCubes.Contains(side)))
                        {
                            sidesToIterate.SetDirect(side, cubeCount.counter.Value);
                        }
                    }
                }
            }

            var back = new HashSetCount();
            foreach ((Cube cube, int value) in sidesToKeep.Select(x => (x, sides.GetCount(x))))
            {
                back.SetDirect(cube, value);
            }

            return back;
        }

        public static Cube FindLowest(IEnumerable<Cube> cubes)
        {
            Cube lowest = cubes.First();

            foreach (var cube in cubes)
            {
                if (cube < lowest)
                    lowest = cube;
            }

            return lowest;
        }
    }
}
