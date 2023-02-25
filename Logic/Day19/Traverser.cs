namespace Logic.Day19
{
    public class Traverser
    {
        private int _currentBest = 0;

        private BluePrint _bluePrint;

        private Stack<CurrentSituation> _queue = new Stack<CurrentSituation>();

        private static readonly RobotType[] robotTypes = { RobotType.Geode, RobotType.Ore, RobotType.Obsidian, RobotType.Clay };

        public Traverser(BluePrint bluePrint, int minutes)
        {
            _bluePrint = bluePrint;
            _queue.Push(new CurrentSituation(new Resources(), new Robots(1, 0, 0, 0), minutes));
        }

        public int Traverse()
        {
            while(_queue.Count > 0)
            {
                var next =_queue.Pop();

                if (next.MinutesLeft == 0)
                {
                    if (next.Resources.Geode > _currentBest)
                    {
                        _currentBest = next.Resources.Geode;
                    }

                    continue;
                }

                if (next.MaxGeode() < _currentBest)
                    continue;

                var canOre = next.CanMakeRobot(RobotType.Ore, _bluePrint);
                var canClay = next.CanMakeRobot(RobotType.Clay, _bluePrint);
                var canObsid = next.CanMakeRobot(RobotType.Obsidian, _bluePrint);
                var canGeode = next.CanMakeRobot(RobotType.Geode, _bluePrint);


                if (canOre)
                {
                    _queue.Push(next.MakeRobot(RobotType.Ore, _bluePrint.OreRobot.ToResourceCount()));
                }

                if (canClay)
                {
                    _queue.Push(next.MakeRobot(RobotType.Clay, _bluePrint.ClayRobot.ToResourceCount()));
                }

                if (canObsid)
                {
                    _queue.Push(next.MakeRobot(RobotType.Obsidian, _bluePrint.ObsidianRobot));
                }

                if (canGeode)
                {
                    _queue.Push(next.MakeRobot(RobotType.Geode, _bluePrint.GeodeRobot));
                }

                _queue.Push(next.MakeStep(canOre, canClay, canObsid, canGeode));
            }

            return _currentBest;
        }
    }
}
