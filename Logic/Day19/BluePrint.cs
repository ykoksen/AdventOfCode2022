namespace Logic.Day19
{
    public enum RobotType { Ore, Clay, Obsidian, Geode};

    public record BluePrint(int OreRobot, int ClayRobot, ResourceCost ObsidianRobot, ResourceCost GeodeRobot);

    public record ResourceCost(int Ore, int Clay, int Obsidian);

    public record struct Resources(int Ore, int Clay, int Obsidian, int Geode)
    {
        public Resources CreateResources(Robots r)
        {
            return this with { Ore = Ore + r.Ore, Clay = Clay + r.Clay, Geode = Geode + r.Geode, Obsidian = Obsidian + r.Obsidian };
        }

        public Resources CreateResourcesAndUse(Robots r, ResourceCost c)
        {
            return this with { Ore = Ore + r.Ore - c.Ore, Clay = Clay + r.Clay - c.Clay, Geode = Geode + r.Geode, Obsidian = Obsidian + r.Obsidian - c.Obsidian};
        }

        public bool Contains(ResourceCost cost)
        {
            return Ore >= cost.Ore && Clay >= cost.Clay && Obsidian >= cost.Obsidian;
        }
    }

    public record struct Robots(int Ore, int Clay, int Obsidian, int Geode)
    {
        public Robots AddRobot(RobotType type) => type switch
        {
            RobotType.Ore => this with { Ore = Ore + 1 },
            RobotType.Clay => this with { Clay = Clay + 1 },
            RobotType.Obsidian => this with { Obsidian = Obsidian + 1 },
            RobotType.Geode => this with { Geode = Geode + 1 },
            _ => throw new NotImplementedException(),
        };

    }

    public record struct CurrentSituation(Resources Resources, Robots Robots, int MinutesLeft)
    {
        public CurrentSituation MakeStep(bool didNotMakeOreRobot, bool didNotMakeClayRobot, bool didNotMakeObsidianRobot, bool didNotMakeGeodeRobot)
        {
            return this with { Resources = Resources.CreateResources(Robots), MinutesLeft = MinutesLeft - 1,
                DidNotMakeOreRobot = didNotMakeOreRobot,
                DidNotMakeClayRobot = didNotMakeClayRobot,
                DidNotMakeObsidianRobot = didNotMakeObsidianRobot,
                DidNotMakeGeodeRobot = didNotMakeGeodeRobot,
            };
        }

        public CurrentSituation MakeRobot(RobotType type, ResourceCost cost)
        {
            return this with { Resources = Resources.CreateResourcesAndUse(Robots, cost), Robots = Robots.AddRobot(type), MinutesLeft = MinutesLeft - 1, DidNotMakeClayRobot = false, DidNotMakeGeodeRobot = false, DidNotMakeObsidianRobot = false, DidNotMakeOreRobot = false };
        }

        public int MaxGeode()
        {
            // Current number of Geodes + Number of Geodes made by current number of Robots in remaining time + production of max number of geode robots that can possibly be created in remaining time
            return Resources.Geode + Robots.Geode * MinutesLeft + (MinutesLeft * (MinutesLeft-1))/2;
        }

        public bool DidNotMakeOreRobot { get; init; } = false;

        public bool DidNotMakeClayRobot { get; init; } = false;

        public bool DidNotMakeObsidianRobot { get; init; } = false;

        public bool DidNotMakeGeodeRobot { get; init; } = false;

        public bool CanMakeRobot(RobotType type, BluePrint bluePrint) => type switch
        {
            RobotType.Ore => !DidNotMakeOreRobot && Resources.Contains(bluePrint.OreRobot.ToResourceCount()),
            RobotType.Clay => !DidNotMakeClayRobot && Resources.Contains(bluePrint.ClayRobot.ToResourceCount()),
            RobotType.Obsidian => !DidNotMakeObsidianRobot && Resources.Contains(bluePrint.ObsidianRobot),
            RobotType.Geode => !DidNotMakeGeodeRobot && Resources.Contains(bluePrint.GeodeRobot),
            _ => throw new NotImplementedException(),
        };
    }

    public static class ResourceCountConvert
    {
        public static ResourceCost ToResourceCount(this int oreCost)
        {
            return new ResourceCost(oreCost, 0, 0);
        }
    }    
}
