using TEdit.Geometry.Primitives;
using GalaSoft.MvvmLight;
using TEditXNA.Terraria;

namespace TEditXna.Editor
{
    public class MouseTile : ObservableObject
    {
        private TileMouseState _mouseState = new TileMouseState();
        private Tile _tile;
        private Vector2Short _uV;
        private string _tileExtras;
        private string _tileName;
        private string _wallName;
        private string _paint;

        public TileMouseState MouseState
        {
            get { return _mouseState; }
            set { Set("MouseState", ref _mouseState, value); }
        }

        public string WallName
        {
            get { return _wallName; }
            set { Set("WallName", ref _wallName, value); }
        }

        public string TileName
        {
            get { return _tileName; }
            set { Set("TileName", ref _tileName, value); }
        }

        public string Paint
        {
            get { return _paint; }
            set { Set("Paint", ref _paint, value); }
        }

        public string TileExtras
        {
            get { return _tileExtras; }
            set { Set("TileExtras", ref _tileExtras, value); }
        }

        public Vector2Short UV
        {
            get { return _uV; }
            set { Set("UV", ref _uV, value); }
        }

        public Tile Tile
        {
            get { return _tile; }
            set
            {
                Set("Tile", ref _tile, value);

                if (World.TileProperties.Count > _tile.Type)
                    TileName = _tile.IsActive ? string.Format("{0} ({1})", World.TileProperties[_tile.Type].Name, _tile.Type) : "[empty]";
                else
                    TileName = string.Format("INVALID TILE ({0})", _tile.Type);

                if (World.WallProperties.Count > _tile.Wall)
                    WallName = string.Format("{0} ({1})", World.WallProperties[_tile.Wall].Name, _tile.Wall);
                else
                    WallName = string.Format("INVALID WALL ({0})", _tile.Wall);

                UV = new Vector2Short(_tile.U, _tile.V);
                if (_tile.LiquidAmount > 0)
                {
                    TileExtras = string.Format("{0}: {1}", _tile.LiquidType, _tile.LiquidAmount);
                }
                else
                    TileExtras = string.Empty;
                
                if (_tile.TileColor > 0)
                {
                    if (_tile.WallColor > 0)
                        Paint = string.Format("Tile: {0}, Wall: {1}", World.PaintProperties[_tile.TileColor].Name, World.PaintProperties[_tile.WallColor].Name);
                    else
                        Paint = string.Format("Tile: {0}", World.PaintProperties[_tile.TileColor].Name);
                }
                else if (_tile.WallColor > 0)
                {
                    Paint = string.Format("Wall: {0}", World.PaintProperties[_tile.WallColor].Name);
                }
                else
                {
                    Paint = "None";
                }

                if (_tile.InActive)
                {
                    TileExtras += " Inactive";
                }

                if (_tile.Actuator)
                {
                    TileExtras += " Actuator";
                }

                if (_tile.WireRed)
                {
                    if (!string.IsNullOrWhiteSpace(TileExtras))
                        TileExtras += ", Wire";
                    else
                        TileExtras += "Wire";
                }
            }
        }
    }
}