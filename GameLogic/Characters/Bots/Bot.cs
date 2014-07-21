using GameLogic.Enums;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character
    {
        protected Bot(Alliance alliance) : base(alliance)
        {
            SetHealth(100);
        }
    }
}
