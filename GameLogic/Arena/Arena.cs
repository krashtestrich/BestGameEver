using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Actions;
using GameLogic.Actions.Movements;
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

        #region Battle

        private void BotPerformAttack(ICharacter c, ArenaFloorTile tile)
        {
            //TODO: Implement logic where bot decides what best thing to do here is...
            var attackAction = c.TargetTileAndSelectActions(tile).FirstOrDefault(a => a is Attack) as Attack;
            if (attackAction != null)
            {
                attackAction.PerformAction((Character)c);
            }
            else
            {
                //TODO: Change this - if bot cannot attack, then move closer to opponent.
                BotPerformMove(c, tile);
            }
        }

        private void BotPerformMove(ICharacter c, ArenaFloorTile tile)
        {
            // Get movement with most distance.
            var moveAction = c.GetActions(false)
                .Where(a => a is Move)
                .OrderByDescending(a => ((Move) a).Distance)
                .FirstOrDefault() as Move;

            if (moveAction == null) return;
            // Get as close to player as possible - find movement that does this.
            var d = moveAction.Distance;
            var newPosition = ArenaHelper.GetClosestMovablePosition(c.ArenaLocation.GetTileLocation(), tile.GetTileLocation(), d);
            var newTile = ArenaFloor[newPosition.XCoord, newPosition.YCoord];
            //TODO: Implement logic for bot moving around obstacles? For now don't allow movement onto tiles that have entities
            var actions = c.TargetTileAndSelectActions(newTile);
            if (actions.Exists(i => i.Name == moveAction.Name))
            {
                moveAction.PerformAction((Character) c);
            }
        }

        protected internal void BotSelectTile(ICharacter c, ArenaFloorTile tile)
        {
            // Have we selected a Tile an opponent is on?
            var entity = tile.GetTileEntity();
            var isOpponent = entity is ICharacter && ((ICharacter)entity).GetAlliance() != c.GetAlliance();
            //TODO: Implement logic where bot decides what best thing to do here is...
            if (!isOpponent)
            {
                //TODO: Change this - for now just assume all bot can do is move.
                BotPerformMove(c, tile);
            }
            else
            {
                //TODO: Change this - for now assume all bot will do is attack enemy.
                BotPerformAttack(c, tile);
            }
        }

        #endregion

        public Arena()
        {
            Characters = new List<ICharacter>();
        }

        #endregion
    }
}
