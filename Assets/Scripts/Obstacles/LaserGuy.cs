using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGuy : MonoBehaviour
{
    public enum AdvancementType
    {
        PlayerMovement,
        TimeMovement
    }

    [Tooltip("What makes the laser guy tick its clock?")]
    public AdvancementType advancementType;
    [Tooltip("How long the laser is active for")]
    public int numTicksActive = 1;
    [Tooltip("How long the laser is inactive for")]
    public int numTicksInactive = 3;

    [Tooltip("Can be used to stagger multiple laser guys")]
    public int numTicksInitialDelay = 0;

    private TimeResponder timeResponder;
    private PlayerMovementResponder playerMovementResponder;
    private LineRenderer laserRenderer;

    private int totalTimeRunning;

    private void Awake()
    {
        timeResponder = GetComponent<TimeResponder>();
        playerMovementResponder = GetComponent<PlayerMovementResponder>();
        laserRenderer = GetComponent<LineRenderer>();

        timeResponder.onTimeChanged.AddListener(OnTimeChanged);
        playerMovementResponder.onPlayerMoved.AddListener(OnPlayerMoved);

        totalTimeRunning = -numTicksInitialDelay;
        RefreshLaserVisibility();
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        if (advancementType == AdvancementType.TimeMovement)
        {
            // this guy works independently of the player's time, so we'll just advance _forward_ by the number of steps the player has taken (usually 1 at a time but let's cover all cases)
            totalTimeRunning += Mathf.Abs(offset);
            RefreshLaserVisibility();
        }
    }

    private void OnPlayerMoved(int numSteps)
    {
        if (advancementType == AdvancementType.PlayerMovement)
        {
            totalTimeRunning += numSteps;
            RefreshLaserVisibility();
        }
    }

    private void RefreshLaserVisibility()
    {
        if (totalTimeRunning >= 0)
        {
            if ((totalTimeRunning % (numTicksActive + numTicksInactive)) < numTicksActive)
                laserRenderer.enabled = true;
            else
                laserRenderer.enabled = false;
        }
        else
            laserRenderer.enabled = false;
    }
}
