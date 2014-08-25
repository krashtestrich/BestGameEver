namespace GameLogic.Actions.Attacks
{
    public class RecklessSwing : AttackBase, IAction, IAttack
    {
        public override string Name
        {
            get { return "Reckless Swing"; }
        }

        public override int DamageFromModifier
        {
            get { return 10; }
        }

        public override int DamageToModifier
        {
            get { return 50; }
        }

        public override int MinRange
        {
            get { return 1; }
        }

        public override int MaxRange
        {
            get { return 1; }
        }
    }
}
