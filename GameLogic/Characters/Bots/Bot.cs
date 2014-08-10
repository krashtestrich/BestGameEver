using GameLogic.Enums;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character, IBot
    {
        protected Bot(Alliance alliance, int level)
            : base(alliance, level)
        {
            SetHealth(100);
        }

        public abstract int Worth { get; }
    }
}
