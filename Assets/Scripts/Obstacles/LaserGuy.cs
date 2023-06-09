using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public float laserHitboxThickness = 0.2f;

    private TimeResponder timeResponder;
    private PlayerResponder playerMovementResponder;
    private LineRenderer laserRenderer;

    private int totalTimeRunning;

    public bool isShooting { get; private set; }

    public UnityEvent<int, int> onTick;

    private void Awake()
    {
        timeResponder = GetComponent<TimeResponder>();
        playerMovementResponder = GetComponent<PlayerResponder>();
        laserRenderer = GetComponent<LineRenderer>();

        timeResponder.onTimeChanged.AddListener(OnTimeChanged);
        playerMovementResponder.onPlayerMoved.AddListener(OnPlayerMoved);

        totalTimeRunning = -numTicksInitialDelay;
        RefreshLaser();
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        if (advancementType == AdvancementType.TimeMovement)
        {
            // this guy works independently of the player's time, so we'll just advance _forward_ by the number of steps the player has taken (usually 1 at a time but let's cover all cases)
            totalTimeRunning += Mathf.Abs(offset);
            RefreshLaser();
        }
    }

    private void OnPlayerMoved(int numSteps)
    {
        if (advancementType == AdvancementType.PlayerMovement)
        {
            totalTimeRunning += numSteps;
            RefreshLaser();
        }
    }

    private void RefreshLaser()
    {
        if (totalTimeRunning >= 0)
        {
            int t = (totalTimeRunning % (numTicksActive + numTicksInactive));
            if (t < numTicksActive)
                isShooting = true;
            else
                isShooting = false;

            int timeTilShoot = Mathf.Max(t, 0);
            onTick?.Invoke(timeTilShoot, numTicksInactive);
        }
        else
            isShooting = false;

        if (isShooting)
        {
            // kill the player if it's within reasonable range of the laser
            Vector3 laserDirection = transform.forward;
            Player player = Player.singleton;
            if (player)
            {
                Vector3 perpendicularToLaser = Vector3.Cross(laserDirection, Vector3.up).normalized;

                if (Mathf.Abs(Vector3.Dot(player.effectivePosition - transform.position, perpendicularToLaser)) <= laserHitboxThickness)
                {
                    player.Die();
                }
            }
        }

        laserRenderer.enabled = isShooting;
    }
}
