using System;

namespace CSharpWarrior
{
    public class LevelFactory
    {
        private static readonly Func<Level>[] Factories =
        {
            () => new Level(
                new Location(),
                new Location(),
                new Location(),
                new Location(),
                new Location()
                ),
            () => new Level(
                new Location(),
                new Location(),
                new Location(),
                new Location(new Door("Open Sesame")),
                new Location()
                )
        };

        public Level MakeLevel(int index)
        {
            index--;
            if(index >= 0 && index < Factories.Length)
                return Factories[index]();

            throw new ArgumentOutOfRangeException("index");
        }
    }
}
