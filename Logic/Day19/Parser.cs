using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Input;

namespace Logic.Day19
{
    public static class Parser
    {
        public static async Task<List<BluePrint>> Parse()
        {
            using var reader = Loader.LoadReader(19);

            var back = new List<BluePrint>();

            while(!reader.EndOfStream)
            {
                var line = (await reader.ReadLineAsync())!;

                var splits = line.Split(" ");

                if (splits is ["Blueprint", var number, "Each", "ore", "robot", "costs", var oreCost, "ore.", "Each", "clay", "robot", "costs", var clayCosts, "ore.", "Each", "obsidian", "robot", "costs", var obsOreCost, "ore", "and", var obsClayCost, "clay.", "Each", "geode", "robot", "costs", var geodeOreCost, "ore", "and", var geodeObsCosts, "obsidian."])                    
                {
                    back.Add(new BluePrint(int.Parse(oreCost), int.Parse(clayCosts), new ResourceCost(int.Parse(obsOreCost), int.Parse(obsClayCost), 0), new ResourceCost(int.Parse(geodeOreCost), 0, int.Parse(geodeObsCosts))));
                }
            }

            return back;
        }
    }
}
