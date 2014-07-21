namespace GameLogic.Arena
{
    public class ArenaFloorTile
    {
        private IGameEntity _entity;
        private readonly ArenaFloorPosition _arenaFloorPosition;

        public ArenaFloorTile(int xLoc, int yLoc)
        {
            _arenaFloorPosition = new ArenaFloorPosition(xLoc, yLoc);
        }

        public void AddEntityToTile(IGameEntity entity)
        {
            _entity = entity;
            entity.SetEntityLocation(this);
        }

        public void RemoveEntityFromTile(IGameEntity entity)
        {
            _entity = null;
        }

        public IGameEntity GetTileEntity()
        {
            return _entity;
        }

        public ArenaFloorPosition GetTileLocation()
        {
            return _arenaFloorPosition;
        }
    }
}
