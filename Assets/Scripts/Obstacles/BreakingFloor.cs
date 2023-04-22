using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingFloor : MonoBehaviour
{
    private TimeResponder timeResponder;

    [Tooltip("What time the floor breaks")]
    public int breakTime;

    private bool isBroken;

    private void Awake()
    {
        timeResponder = GetComponent<TimeResponder>();
        timeResponder.onTimeChanged.AddListener(OnTimeChanged);

        UpdateBrokenness(TimeManager.singleton.currentTime);
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        UpdateBrokenness(newTime);
    }

    private void UpdateBrokenness(int currentTime)
    {
        isBroken = currentTime >= breakTime;

        // just disappear for now, we can do more later
        GetComponentInChildren<Renderer>().enabled = !isBroken;
    }
}
