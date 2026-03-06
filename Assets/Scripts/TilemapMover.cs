using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMover : MonoBehaviour
{
    [SerializeField] private Tilemap source;
    [SerializeField] private Tilemap target;

    private void Start()
    {
        MoveTilesWithoutColliders(source, target);
    }

    private void MoveTilesWithoutColliders(Tilemap source, Tilemap target)
    {
        foreach (var pos in source.cellBounds.allPositionsWithin) {

            TileBase tile = source.GetTile(pos);
            if (tile == null) continue;
            TileData data = new();
            tile.GetTileData(pos, source, ref data);

            if (data.colliderType == Tile.ColliderType.None)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = data.sprite;
                target.SetTile(pos, newTile);
                Destroy(tile);

                //target.SetTile(pos, new TileBase());
            }
        
        }
    }

}
