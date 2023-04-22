using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerResponder))]
public class MoveTimeWhenSteppedOn : MonoBehaviour
{
    private PlayerResponder playerResponder;

    public int timeOffsetToTick = 1;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();

        playerResponder.onSteppedOnByPlayer.AddListener(() => TimeManager.singleton.StepTimeBy(timeOffsetToTick));
    }
}
