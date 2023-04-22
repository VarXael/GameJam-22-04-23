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
    public Level[] levelsPrefab;
    public float tileSize = 1f;


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
    /// Snaps position to an arbitrary grid (horizontally)
    /// </summary>
    public static Vector3 GetSnappedPosition(Vector3 position, float tileSize)
    {
        return new Vector3((int)(position.x / tileSize + 0.5f) * tileSize, position.y, (int)(position.z / tileSize + 0.5f) * tileSize);
    }
}
