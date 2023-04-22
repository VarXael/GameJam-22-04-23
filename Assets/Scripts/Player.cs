using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player singleton
    {
        get
        {
            if (_singleton == null)
                _singleton = FindObjectOfType<Player>();
            return _singleton;
        }
    }
    private static Player _singleton;

    [Tooltip("Duration of a single move in seconds")]
    public float moveDuration = 0.5f;
    [Tooltip("Animation of movement between tiles")]
    public AnimationCurve movementCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    // Timey wimey stuff
    public bool movingChangesTime = true;
    public Vector3 positionOfZeroTime = Vector3.zero;

    public bool isDead { get; private set; }

    private bool isMoving;
    private float moveProgess = 1f;
    private Vector3 moveTargetPosition;
    private Vector3 moveSourcePosition;

    public Vector3 effectivePosition => isMoving ? moveTargetPosition : transform.position;

    void Update()
    {
        if (!isMoving)
        {
            // todo maybe don't do this every frame?

            // todo: might adjust based on camera if necessary
            Vector3 movementIntent = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            // decide whether to move
            if (movementIntent.x != 0 || movementIntent.z != 0) // if user is pressing diagonally don't move them at all I guess
            {
                // hmm I guess since this is a tile-based game we only want to move one axis at a time (new to me)
                if (movementIntent.x != 0)
                {
                    moveTargetPosition = new Vector3(transform.position.x + LevelManager.singleton.tileSize * Mathf.Sign(movementIntent.x), transform.position.y, transform.position.z);
                }
                else if (movementIntent.z != 0)
                {
                    moveTargetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + LevelManager.singleton.tileSize * Mathf.Sign(movementIntent.z));
                }

                moveSourcePosition = transform.position;
                moveTargetPosition = LevelManager.singleton.GetSnappedPosition(moveTargetPosition);
                transform.rotation = Quaternion.LookRotation(moveTargetPosition - moveSourcePosition);

                moveProgess = 0f;
                isMoving = true;

                if (movingChangesTime)
                {
                    // advance/reverse time
                    int targetTime = Mathf.RoundToInt(moveTargetPosition.x - positionOfZeroTime.x);
                    if (targetTime != TimeManager.singleton.currentTime)
                    {
                        TimeManager.singleton.StepTimeBy(targetTime - TimeManager.singleton.currentTime);
                    }
                }

                // hack cus FindObjectsOfType is bad: enact player movement responders now
                foreach (var movementResponder in FindObjectsOfType<PlayerResponder>())
                    movementResponder.onPlayerMoved?.Invoke(1);

                LevelManager.singleton.OnPlayerSteppedTo(moveTargetPosition);
            }
        }
        else
        {
            // blend towards target position
            moveProgess += moveDuration > 0f ? Time.deltaTime / moveDuration : 1f - moveProgess;

            transform.position = Vector3.Lerp(moveSourcePosition, moveTargetPosition, movementCurve.Evaluate(moveProgess));

            if (moveProgess >= 1f)
            {
                transform.position = LevelManager.singleton.GetSnappedPosition(transform.position);
                isMoving = false;
            }
        }
    }

    public void Die(string reason = "old age")
    {
        isDead = true;
        foreach (var playerResponder in FindObjectsOfType<PlayerResponder>())
        {
            playerResponder.onPlayerDied?.Invoke(reason);
        }

        // todo massive explosion VFX?
        gameObject.SetActive(false);
    }
}
