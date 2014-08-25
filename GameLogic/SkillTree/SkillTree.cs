using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.Helpers;
using GameLogic.SkillTree.Paths;

namespace GameLogic.SkillTree
{
    public class SkillTree
    {
        private readonly List<IPath> _skills;

        public void TakeSkill(IPath skill, ICharacter c)
        {
            if (skill.Parent != null && _skills.Exists(i => i.Name == skill.Parent.Name && !i.IsActive))
            {
                throw new Exception("You cannot take this skill yet!");
            }
            if (skill.Level == 1 && _skills.Exists(i => i.IsActive && i.Level == 1))
            {
                throw new Exception("You can only have one level one skill!");
            }

            _skills.First(i => i.Name == skill.Name).ActivateSkill(c);
        }

        public SkillTree()
        {
            _skills = new InstanceRetriever<IPath>().RetrieveInstances().ToList();
        }

        public List<IPath> Get()
        {
            return _skills;
        }
    }
}
