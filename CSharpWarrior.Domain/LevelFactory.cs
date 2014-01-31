namespace CSharpWarrior
{
    public class LevelFactory
    {
        public Level MakeLevel1()
        {
            return new Level(
                new Location(), 
                new Location(), 
                new Location(), 
                new Location(),
                new Location()
                );
        }
        public Level MakeLevel2()
        {
            return new Level(
                new Location(), 
                new Location(), 
                new Location(), 
                new Location(new Door("Open Sesame")),
                new Location()
                );
        }
    }
}
