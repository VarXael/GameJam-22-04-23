using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponder : MonoBehaviour
{
    public UnityEvent<int> onPlayerMoved;

    public UnityEvent<string> onPlayerDied;

    /// <summary>
    /// Only gets called if you have a Tile component
    /// </summary>

    public UnityEvent onSteppedOnByPlayer;
}
