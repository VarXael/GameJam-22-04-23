using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    /// <summary>
    /// the single instance of the time manager
    /// </summar>
    public static TimeManager singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<TimeManager>();
            }
            return _singleton;
        }
    }
    private static TimeManager _singleton;

    /// <summary>
    /// if enabled, Q/E steps time forward/backwards for debugging
    /// </summary>
    public bool enableDebugMode = true;

    // some temporary hard-coded constraints, time can't go beyond this range
    public int maxTime = 10;
    public int minTime = 0;

    // current time in terms of the robot's clock
    public int currentTime { get; private set; }

    // all known time responders in the level at the moment
    public readonly List<TimeResponder> timeResponders = new List<TimeResponder>();

    public void Update()
    {
        // run debug stuff
        if (enableDebugMode)
        {
            if (Input.GetKeyDown(KeyCode.E))
                StepTimeBy(1);
            if (Input.GetKeyDown(KeyCode.Q))
                StepTimeBy(-1);
        }
    }

    /// <summary>
    /// steps time by the given offset
    /// </summary>
    public void StepTimeBy(int timeOffset)
    {
        int nextTime = Mathf.Clamp(currentTime + timeOffset, minTime, maxTime);

        if (nextTime != currentTime)
        {
            int actualOffset = nextTime - currentTime; // actual offset might not be the same as the attempted offset if it hits the clamp
            currentTime = nextTime;

            foreach (TimeResponder responder in timeResponders)
            {
                if (responder)
                    responder.OnTimeChanged(currentTime, actualOffset);
                else
                    Debug.LogWarning($"A time responder has been unexpectedly removed or destroyed");
            }
        }
        else
        {
            Debug.LogWarning($"StepTimeBy({timeOffset}) didn't step at all, we either hit a limit or no offset set");
        }
    }

    // regitster/unregister time responders, so that we can keep track of anything that cares about time
    public void RegisterTimeResponder(TimeResponder responder)
    {
        timeResponders.Add(responder);
    }

    public void UnregisterTimeResponder(TimeResponder responder)
    {
        timeResponders.Remove(responder);
    }
}
