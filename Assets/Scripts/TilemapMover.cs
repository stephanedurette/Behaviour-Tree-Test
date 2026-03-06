using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMover : MonoBehaviour
{
    [SerializeField] private Tilemap source;
    [SerializeField] private Tilemap target;

    private void Start()
    {
        MoveTiles();
    }

    private void MoveTiles()
    {
        for (int x = 0; x < source.size.x; x++) { 
            for (int y = 0; y < source.size.y; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                TileBase tile = source.GetTile(position);
                if (tile == null) continue;
                TileData data = new();
                tile.GetTileData(position, source, ref data);

                if (data.colliderType == Tile.ColliderType.None) {
                    target.SetTile(position, tile);
                }

                Debug.Log(data.colliderType);
            }
        }
    }

}
