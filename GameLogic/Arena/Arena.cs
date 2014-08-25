using System;
using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Enums;

namespace GameLogic.Arena
{
    public class Arena
    {
        #region Constructors
        #region Arena
        public void ResetArena()
        {
            Characters = new List<ICharacter>();
            BuildArenaFloor(5);
        }

        #region Arena Floor

        public ArenaFloorTile[,] ArenaFloor { get; private set; }

        public void BuildArenaFloor(int width)
        {
            ArenaFloor = new ArenaFloorTile[width, width];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    ArenaFloor[i,j] = new ArenaFloorTile(i,j);
                }
            }
        }

        public ArenaFloorTile[,] GetArenaFloor()
        {
            return ArenaFloor;
        }

        public ArenaFloorTile SelectFloorTile(ArenaFloorPosition pos)
        {
            return ArenaFloor[pos.XCoord,pos.YCoord];
        }

        #endregion

        #endregion

        #region Characters

        public List<ICharacter> Characters { get; private set; }

        public void AddCharacterToArena(ICharacter c, Alliance alliance, int? xLoc = null, int? yLoc = null)
        {
            if (ArenaFloor == null)
            {
                throw new Exception("Arena not been constructed!");
            }
            c.ChangeAlliance(alliance);
            Characters.Add(c);

            if (xLoc == null || yLoc == null)
            {
                SetDefaultCharacterLocation(c);
            }
            else
            {
                ArenaFloor[(int)xLoc, (int)yLoc].AddEntityToTile((Character)c);
            }
        }

        private void SetDefaultCharacterLocation(ICharacter c)
        {
            var xLoc = c.GetAlliance() == Alliance.TeamOne ? 0 : (ArenaFloor.GetLength(0) - 1);
            var yLoc = c.GetAlliance() == Alliance.TeamOne ? (ArenaFloor.GetLength(1) - 1) : 0;
            ArenaFloor[xLoc, yLoc].AddEntityToTile((Character)c);
        }

        #endregion

        public Arena()
        {
            Characters = new List<ICharacter>();
        }

        #endregion
    }
}
