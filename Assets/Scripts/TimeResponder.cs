using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Indicates that this object cares about time. Scripts can subscribe to onTimeChanged to respond appropriately
/// </summary>
public class TimeResponder : MonoBehaviour
{
    /// <summary>
    /// Called when time changes. First param is the new time, second param is the offset from the previous time
    /// </summary>
    public UnityEvent<int, int> onTimeChanged;

    private void OnEnable()
    {
        TimeManager.singleton.RegisterTimeResponder(this);
    }

    private void OnDisable()
    {
        if (TimeManager.singleton)
            TimeManager.singleton.UnregisterTimeResponder(this);
    }

    public void OnTimeChanged(int newTime, int offset)
    {
        onTimeChanged.Invoke(newTime, offset);
    }
}
