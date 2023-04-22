using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer(transform.position);
    }

    //Spawns Player at default position, given by the grid
    //public void SpawnPlayer()
    //{
    //    Instantiate(playerGO, new Vector3(Mathf.Clamp(defaultPlayerPosition.y - 1, 0, gridSize.y - 1), 0, Mathf.Clamp(defaultPlayerPosition.y - 1, 0, gridSize.y - 1)), playerGO.transform.rotation);
    //    Destroy(gameObject);
    //}

    //Spawns Player in the Level at a specified position passed by reference
    public void SpawnPlayer(Vector3 specifiedSpawnPosition)
    {
        Instantiate(playerGO, specifiedSpawnPosition, playerGO.transform.rotation);
        Destroy(gameObject);
    }
}
