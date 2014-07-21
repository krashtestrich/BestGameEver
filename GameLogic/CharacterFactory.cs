using GameLogic.Characters;
using GameLogic.Enums;

namespace GameLogic
{
    public static class CharacterFactory
    {
        public static ICharacter Character
        {
            get
            {
                return _character ?? new Character(Alliance.Player);
            }
            set
            {
                _character = value;
            }
        }

        private static ICharacter _character;
    }
}
