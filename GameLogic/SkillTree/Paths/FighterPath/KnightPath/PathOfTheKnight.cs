﻿using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;
using GameLogic.Modifiers;
using GameLogic.Modifiers.Character.Armor;
using GameLogic.Modifiers.Character.Health;

namespace GameLogic.SkillTree.Paths.FighterPath.KnightPath
{
    public class PathOfTheKnight : SkillBase, IPath
    {
        public string Name
        {
            get { return "Path of the Knight"; }
        }

        public int Cost
        {
            get { return 1; }
        }

        public int Level
        {
            get { return 2; }
        }

        public SkillBranches Path
        {
            get
            {
                return SkillBranches.Knight;
            }
        }

        public override IPath Parent
        {
            get
            {
                return new PathOfTheFighter();
            }
        }

        public override List<IModifier<ICharacter>> CharacterModifiers
        {
            get
            {
                return new List<IModifier<ICharacter>>
                {
                    new HealthBonusPercentage(20),
                    new ArmorBonusPercentage(25)
                };
            }
        }

        public SkillBranches BasePath
        {
            get { return SkillBranches.Fighter; }
        }
    }
}
