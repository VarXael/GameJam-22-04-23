using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerResponder : MonoBehaviour
{
    public UnityEvent<int> onPlayerMoved;

    public UnityEvent<string> onPlayerDied;
}
