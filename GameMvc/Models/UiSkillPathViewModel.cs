using System.Collections.Generic;
using GameLogic.SkillTree.Paths;

namespace GameMvc.Models
{
    public class UiSkillPathViewModel
    {
        public IPath CurrentPath;
        public List<IPath> Paths { get; set; }
    }
}