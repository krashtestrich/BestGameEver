using GameLogic.Enums;

namespace GameLogic.Characters.Bots
{
    public class Dumbass : Bot
    {
        public Dumbass(Alliance alliance, int level)
            : base(alliance, level)
        {
            SetName("Dumbass");
            SetCash(100);
        }

        public override int Worth
        {
            get { return 50; }
        }
    }
}
