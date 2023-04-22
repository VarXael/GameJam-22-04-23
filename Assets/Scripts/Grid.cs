using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid : MonoBehaviour
{
    public GameObject gridTileGO;
    public GameObject playerGO;
    public Vector2 gridSize;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
