using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager singleton
    {
        get
        {
            if (_singleton == null)
                _singleton = FindObjectOfType<LevelManager>();
            return _singleton;
        }
    }
    private static LevelManager _singleton;

    public GameObject gridTileGO;
    public GameObject playerGO;
    public Level[] levelsPrefab;
    public float tileSize = 1f;

    [Tooltip("The level to start on")]
    public int startLevelIndex = 0;

    private Level spawnedLevel;
    private int spawnedLevelIndex = 0;

    private List<PlayerResponder> playerResponders = new List<PlayerResponder>();

    void Start()
    {
        SpawnLevel(startLevelIndex);
    }

    //Spawns grid, by default it takes a random grid in the levelsPrefab array and spawns that in.
    public void SpawnRandomLevel() => SpawnLevel(Random.Range(0, levelsPrefab.Length));
    //Overload method of the spawn grid that makes a specific grid with index spawn into the scene.
    private void SpawnLevel(int chosenGrid)
    {
        if (spawnedLevel != null)
        {
            Destroy(spawnedLevel.gameObject);
            spawnedLevel = null;
        }

        spawnedLevel = Instantiate(levelsPrefab[chosenGrid], Vector3.zero, levelsPrefab[Random.Range(0, levelsPrefab.Length)].transform.rotation);
        spawnedLevelIndex = chosenGrid;
    }

    /// <summary>
    /// Restarts the current level by destroying/respawning it
    /// </summary>
    public void RestartLevel() => SpawnLevel(spawnedLevelIndex);

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
    public void RegisterTile(PlayerResponder tile)
    {
        playerResponders.Add(tile);
    }

    public void UnregisterTile(PlayerResponder tile)
    {
        playerResponders.Remove(tile);
    }

    /// <summary>
    /// Called when the player steps to a tile. Sends notification to relevant tiles that the player has stepped on them
    /// </summary>
    public void OnPlayerSteppedTo(Vector3 targetPosition)
    {
        float distanceCheckRange = tileSize * 0.5f;
        foreach (PlayerResponder playerResponder in playerResponders)
        {
            // hack: basic distance check, it'll be fine.
            if (Vector3.Distance(targetPosition, playerResponder.transform.position) < distanceCheckRange)
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
