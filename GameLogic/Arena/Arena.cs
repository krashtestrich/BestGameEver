using System;
using System.Collections.Generic;
using GameLogic.Characters;
using GameLogic.Characters.Player;

namespace GameLogic.Arena
{
    public class Arena
    {
        #region Constructors
        #region Arena
        public void ResetArena()
        {
            Characters = new List<ICharacter>();
            _arenaFloor = null;
        }

        #region Arena Floor
        private ArenaFloorTile[,] _arenaFloor;

        public ArenaFloorTile[,] ArenaFloor
        {
            get
            {
                return _arenaFloor;
            }
        }
  
        public void BuildArenaFloor(int width)
        {
            _arenaFloor = new ArenaFloorTile[width, width];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    _arenaFloor[i,j] = new ArenaFloorTile(i,j);
                }
            }
        }

        public ArenaFloorTile[,] GetArenaFloor()
        {
            return _arenaFloor;
        }

        public ArenaFloorTile SelectFloorTile(ArenaFloorPosition pos)
        {
            return _arenaFloor[pos.XCoord,pos.YCoord];
        }

        #endregion

        #endregion

        #region Characters

        public List<ICharacter> Characters { get; private set; }

        public ICharacter Player { get; private set; }

        public void AddCharacterToArena(ICharacter c, int? xLoc = null, int? yLoc = null)
        {
            if (_arenaFloor == null)
            {
                throw new Exception("Arena not been constructed!");
            }
            if (c.GetType() == typeof(Player))
            {
                Player = c;
            }
            else
            {
                Characters.Add(c);
            }

            if (xLoc == null || yLoc == null)
            {
                SetDefaultCharacterLocation(c);
            }
            else
            {
                c.SetCharacterLocation((int)xLoc, (int)yLoc);
                ArenaFloor[(int)xLoc, (int)yLoc].AddEntityToTile((Character)c);
            }
        }

        private void SetDefaultCharacterLocation(ICharacter c)
        {
            var xLoc = _arenaFloor.GetLength(0) - 1;
            var yLoc = (int)Math.Floor((double)((_arenaFloor.GetLength(1) - 1) / 2));
            c.SetCharacterLocation(xLoc, yLoc);
            ArenaFloor[xLoc,yLoc].AddEntityToTile((Character)c);
        }

        #endregion

        #region Battle

        private Battle.Battle _battle;
        #endregion

        public Arena(Battle.Battle battle)
        {
            _battle = battle;
            Characters = new List<ICharacter>();
        }

        #endregion
    }
}
