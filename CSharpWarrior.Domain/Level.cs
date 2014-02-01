using System;
using System.Linq;

namespace CSharpWarrior
{
    [Serializable]
    public class Level
    {
        private readonly Location[] locations;
        private int warriorLocationIndex;

        public Level(params Location[] locations)
        {
            if (locations.Length < 2)
            {
                throw new ArgumentException("Must have at least two locations", "locations");
            }
            this.locations = locations;
        }

        public Location WarriorPosition
        {
            get { return locations[warriorLocationIndex]; }
        }

        public void MoveWarrior()
        {
            warriorLocationIndex++;
        }

        public Location ExitPosition
        {
            get { return locations.Last(); }
        }

        public void ActOut(WarriorAction action)
        {
            WarriorPosition.TryHandleBefore(action);
            action.Act(this);
            WarriorPosition.TryHandleAfter(action);
        }
    }
}
