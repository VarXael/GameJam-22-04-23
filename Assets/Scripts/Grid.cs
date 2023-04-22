using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public static Grid singleton
    {
        get
        {
            if (_singleton == null)
                _singleton = FindObjectOfType<Grid>();
            return _singleton;
        }
    }
    private static Grid _singleton;

    public GameObject gridTileGO;
    public GameObject playerGO;
    public Vector2 gridSize;
    public float tileSize = 1f;
    public Vector2 playerStart;


    void Start()
    {
        Instantiate(playerGO, new Vector3(Mathf.Clamp(playerStart.y - 1, 0, gridSize.y - 1), 0, Mathf.Clamp(playerStart.y - 1, 0, gridSize.y - 1)), playerGO.transform.rotation);

        for (int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Instantiate(gridTileGO, new Vector3(x, 0, y), gridTileGO.transform.rotation);
            }
        }
    }

    /// <summary>
    /// Snaps position to the grid
    /// </summary>
    public Vector3 GetSnappedPosition(Vector3 position)
    {
        // just call the static one with the customisable tile size, except this tile size
        return GetSnappedPosition(position, tileSize);
    }

    /// <summary>
    /// Snaps position to an arbitrary grid (horizontally)
    /// </summary>
    public static Vector3 GetSnappedPosition(Vector3 position, float tileSize)
    {
        return new Vector3((int)(position.x / tileSize + 0.5f) * tileSize, position.y, (int)(position.z / tileSize + 0.5f) * tileSize);
    }
}
