using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class CustomToolWindow : EditorWindow
{
    // Add a menu item to the Unity Editor's top bar to open the window
    [MenuItem("Tools/Hacks")]
    public static void ShowWindow()
    {
        // Open the window, or create a new one if it doesn't exist
        GetWindow<CustomToolWindow>("Hacks");
    }

    // This method is called to draw the GUI in the window
    void OnGUI()
    {
        if (GUILayout.Button("Move Tilemaps"))
        {
            MoveTilesWithoutColliders();
        }
    }

    private void MoveTilesWithoutColliders()
    {
        Tilemap source = GameObject.Find("Walls").GetComponent<Tilemap>();
        Tilemap target = GameObject.Find("Wall Tops").GetComponent<Tilemap>();

        foreach (var pos in source.cellBounds.allPositionsWithin)
        {

            TileBase tile = source.GetTile(pos);
            if (tile == null) continue;
            TileData data = new();
            tile.GetTileData(pos, source, ref data);

            if (data.colliderType == Tile.ColliderType.None)
            {
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = data.sprite;
                target.SetTile(pos, newTile);
                source.SetColor(pos, Color.clear);
            }
        }
    }
}
