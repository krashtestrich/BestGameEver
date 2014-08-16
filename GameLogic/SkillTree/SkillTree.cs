using System.Collections.Generic;
using System.Linq;
using GameLogic.Characters;
using GameLogic.SkillTree.Health;

namespace GameLogic.SkillTree
{
    public class SkillTree
    {
        private readonly List<SkillBase> _skills;

        public void TakeSkill(string name, ICharacter c)
        {
            _skills.First(i => i.Name == name).ActivateSkill(c);
        }

        public void CancelSkill(string name, ICharacter c)
        {
            _skills.First(i => i.Name == name).DeactivateSkill(c);
        }

        public SkillTree()
        {
            _skills = new List<SkillBase>
            {
                new Healthy()
            };
        }

        public List<SkillBase> Get()
        {
            return _skills;
        }
    }
}
