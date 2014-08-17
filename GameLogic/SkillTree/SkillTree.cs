using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.SkillTree.Health;
using GameLogic.SkillTree.Mana;

namespace GameLogic.SkillTree
{
    public class SkillTree
    {
        private readonly List<SkillBase> _skills;

        public void TakeSkill(SkillBase skill, ICharacter c)
        {
            if (skill.Parent != null && _skills.Exists(i => i.Name == skill.Parent.Name && !i.IsActive))
            {
                throw new Exception("You cannot take this skill yet!");
            }
            _skills.First(i => i.Name == skill.Name).ActivateSkill(c);
        }

        public void CancelSkill(SkillBase skill, ICharacter c)
        {
            _skills.First(i => i.Name == skill.Name).DeactivateSkill(c);
        }

        public SkillTree()
        {
            _skills = new List<SkillBase>
            {
                new HealthLevelOne(),
                new HealthLevelTwo(),
                new ManaLevelOne(),
                new ManaLevelTwo()
            };
        }

        public List<SkillBase> Get()
        {
            return _skills;
        }
    }
}
