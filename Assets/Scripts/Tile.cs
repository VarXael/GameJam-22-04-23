using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnEnable()
    {
        GridManager grid = GridManager.singleton;
        if (grid)
        {
            grid.RegisterTile(this);
        }
    }

    private void OnDisable()
    {
        GridManager grid = GridManager.singleton;
        if (grid)
        {
            grid.UnregisterTile(this);
        }
    }
}
