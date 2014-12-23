using System;
using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Characters;
using GameLogic.Modifiers;
using GameLogic.SkillTree.Paths;

namespace GameLogic.SkillTree
{
    public abstract class SkillBase
    {
        #region Activation / Deactivation
        public bool IsActive { get; private set; }

        public void ActivateSkill(ICharacter c)
        {
            if (IsActive)
            {
                throw new Exception("What!? Skill is already active...");
            }
           
            CharacterModifiers.ForEach(c.AddModifier);
            Actions.ForEach(c.AddAction);
            IsActive = true;
        }
       
        #endregion


        public virtual IPath Parent
        {
            get { return null; }
        }

        public virtual List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>();
            }
        }

        public List<IAction> Actions { get; protected set; }

        protected SkillBase()
        {
            Actions = new List<IAction>();
        }
    }
}
