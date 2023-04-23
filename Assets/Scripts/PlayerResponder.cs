using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponder : MonoBehaviour
{
    public UnityEvent<int> onPlayerMoved;

    public UnityEvent<string> onPlayerDied;

    public UnityEvent onSteppedOnByPlayer;

    [Tooltip("If true, this object won't let the player step on it")]
    public bool blocksPlayer = false;

    private float playerDistanceCheckRange;

    private void OnEnable()
    {
        LevelManager grid = LevelManager.singleton;
        if (grid)
        {
            playerDistanceCheckRange = grid.tileSize * 0.5f; // kinda hack
            grid.RegisterTile(this);
        }
    }

    private void OnDisable()
    {
        LevelManager grid = LevelManager.singleton;
        if (grid)
        {
            grid.UnregisterTile(this);
        }
    }

    public bool DoesOccupyPosition(Vector3 position)
    {
        return Vector3.Distance(position, transform.position) < playerDistanceCheckRange;
    }
}
