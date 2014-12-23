namespace GameLogic.Actions.Spells.Damage
{
    public class MindBlast : DamageBase, IAction, ISpell
    {
        public override string Name
        {
            get { return "Mind blast"; }
        }

        public override int HitsForFrom
        {
            get { return 50; }
        }

        public override int HitsForTo
        {
            get { return 85; }
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
