namespace GameLogic.Actions.Spells.Damage
{
    public class SpellSpear : DamageBase, IAction, ISpell
    {
        public override string Name
        {
            get { return "Spell spear"; }
        }

        public override int HitsForFrom
        {
            get { return 15; }
        }

        public override int HitsForTo
        {
            get { return 25; }
        }

        public override int MinRange
        {
            get { return 1; }
        }

        public override int MaxRange
        {
            get { return 2; }
        }

        public override int ManaCost
        {
            get { return 25; }
        }
    }
}
