using GameLogic.Characters;

namespace GameLogic
{
    public static class CharacterFactory
    {
        public static ICharacter Character
        {
            get
            {
                return _character ?? new Character();
            }
            set
            {
                _character = value;
            }
        }

        private static ICharacter _character;
    }
}
