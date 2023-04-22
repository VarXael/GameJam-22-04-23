using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponder : MonoBehaviour
{
    public UnityEvent<int> onPlayerMoved;

    public UnityEvent<string> onPlayerDied;


    public UnityEvent onSteppedOnByPlayer;

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
