using System.Collections.Generic;
using GameLogic.Actions;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;

namespace GameLogic.SkillTree.Paths
{
    public interface IPath
    {
        string Name { get; }
        bool IsActive { get; }
        int Cost { get; }
        int Level { get; }
        SkillBranches BasePath { get; }
        SkillBranches Path { get; }
        IPath Parent { get; }
        List<IModifier<ICharacter>> CharacterModifiers { get; }
        List<IAction> Actions { get; }

        void ActivateSkill(ICharacter c);
    }
}
