using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnEnable()
    {
        Grid grid = Grid.singleton;
        if (grid)
        {
            grid.RegisterTile(this);
        }
    }

    private void OnDisable()
    {
        Grid grid = Grid.singleton;
        if (grid)
        {
            grid.UnregisterTile(this);
        }
    }
}
