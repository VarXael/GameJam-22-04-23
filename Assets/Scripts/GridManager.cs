using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager singleton
    {
        get
        {
            if (_singleton == null)
                _singleton = FindObjectOfType<GridManager>();
            return _singleton;
        }
    }
    private static GridManager _singleton;

    public GameObject gridTileGO;
    public GameObject playerGO;
    public Level[] levelsPrefab;
    public float tileSize = 1f;

    private List<Tile> tiles = new List<Tile>();

    void Start()
    {
        SpawnGrid();
    }

    //Spawns grid, by default it takes a random grid in the levelsPrefab array and spawns that in.
    public void SpawnGrid()
    {
        Instantiate(levelsPrefab[Random.Range(0, levelsPrefab.Length)], Vector3.zero, levelsPrefab[Random.Range(0, levelsPrefab.Length)].transform.rotation);
    }
    //Overload method of the spawn grid that makes a specific grid with index spawn into the scene.
    private void SpawnGrid(int chosenGrid)
    {
        Instantiate(levelsPrefab[chosenGrid], Vector3.zero, levelsPrefab[Random.Range(0, levelsPrefab.Length)].transform.rotation);
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
    /// Registers a tile with this grid for fast lookup (and can respond to player step on events etc)
    /// </summary>
    public void RegisterTile(Tile tile)
    {
        tiles.Add(tile);
    }

    public void UnregisterTile(Tile tile)
    {
        tiles.Remove(tile);
    }

    /// <summary>
    /// Called when the player steps to a tile. Sends notification to relevant tiles that the player has stepped on them
    /// </summary>
    public void OnPlayerSteppedTo(Vector3 targetPosition)
    {
        float distanceCheckRange = tileSize * 0.5f;
        foreach (Tile tile in tiles)
        {
            // hack: basic distance check, it'll be fine.
            if (Vector3.Distance(targetPosition, tile.transform.position) < distanceCheckRange && tile.TryGetComponent(out PlayerResponder playerResponder))
            {
                playerResponder.onSteppedOnByPlayer?.Invoke();
            }
        }
    }

    /// <summary>
    /// Snaps position to an arbitrary grid (horizontally)
    /// </summary>
    public static Vector3 GetSnappedPosition(Vector3 position, float tileSize)
    {
        return new Vector3((int)(position.x / tileSize + 0.5f) * tileSize, position.y, (int)(position.z / tileSize + 0.5f) * tileSize);
    }
}
