namespace GameLogic.Actions.Attacks
{
	public class Swing : AttackBase, IAction, IAttack
	{
		public override string Name
		{
			get { return "Swing"; }
		}

		public override int DamageFromModifier
		{
			get { return 0; }
		}

		public override int DamageToModifier
		{
			get { return 0; }
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
