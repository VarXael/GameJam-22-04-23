using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingFloor : MonoBehaviour
{
    private TimeResponder timeResponder;
    private PlayerResponder playerResponder;

    [Tooltip("What time the floor breaks")]
    public TimeCondition breakCondition = new TimeCondition();

    private bool isBroken;

    private void Awake()
    {
        timeResponder = GetComponent<TimeResponder>();
        playerResponder = GetComponent<PlayerResponder>();
        timeResponder.onTimeChanged.AddListener(OnTimeChanged);
        playerResponder.onSteppedOnByPlayer.AddListener(OnSteppedOnByPlayer);

        UpdateBrokenness(TimeManager.singleton.currentTime);
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        UpdateBrokenness(newTime);
    }

    private void UpdateBrokenness(int currentTime)
    {
        isBroken = breakCondition.IsTrue(currentTime);

        // just disappear for now, we can do more later
        GetComponentInChildren<Renderer>().enabled = !isBroken;
    }

    private void OnSteppedOnByPlayer()
    {
        if (isBroken)
        {
            // kill the player
            Player.singleton.Die("stepped on broken tile");
        }
    }
}
