using System;
using GameLogic.Arena;
using GameLogic.Characters;

namespace GameLogic.Actions.Movements
{
    public abstract class MoveBase : Action
    {
        public abstract int Distance
        { get; }

        public virtual bool CanBePerformed(IGameEntity source)
        {
            return source.TargettedTile.GetTileEntity() == null 
                && ArenaHelper.GetDistanceBetweenFloorPositions(source.ArenaLocation.GetTileLocation(), source.TargettedTile.GetTileLocation()) <= Distance;
        }

        public virtual string Perform(IGameEntity source)
        {
            var character = source as Character;
            if (character == null)
            {
                throw new Exception("Source cannot be null...");
            }
            source.ArenaLocation.RemoveEntityFromTile(source);
            source.TargettedTile.AddEntityToTile(source);
            return character.Name + " " + Verb + " to " + source.TargettedTile.GetTileLocation().XCoord + "," +
                   source.TargettedTile.GetTileLocation().YCoord;
        }
    }
}
