using System;
using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

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
            IsActive = true;
        }

        public void DeactivateSkill(ICharacter c)
        {
            if (!IsActive)
            {
                throw new Exception("What!? Skill is already inactive...");
            }
            CharacterModifiers.ForEach(c.RemoveModifier);
            IsActive = false;
        }
        #endregion

        public abstract string Name { get; }
        public abstract int Cost { get; }
        public abstract int Level { get; }
        public abstract SkillBranches Branch { get; }

        public virtual SkillBase Parent
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
    }
}
