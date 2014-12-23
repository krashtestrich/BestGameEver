using GameLogic.Enums;

namespace GameLogic.SkillTree.Paths
{
    public class PathOfTheNothing : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Nothing"; }
        }

        public int Cost
        {
            get { return 0; }
        }

        public int Level
        {
            get { return 0; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.NotTaken;
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.NotTaken; }
        }
    }
}
