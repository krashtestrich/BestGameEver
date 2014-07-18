using GameLogic.Enums;

namespace GameLogic.Characters.Bots
{
    public abstract class Bot : Character
    {
        private Alliance _alliance;

        protected Bot(Alliance alliance)
        {
            _alliance = alliance;
        }

        public void ChangeAlliance(Alliance a)
        {
            _alliance = a;
        }

        public Alliance GetAlliance()
        {
            return _alliance;
        }
    }
}
