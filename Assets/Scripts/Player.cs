using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Duration of a single move in seconds")]
    public float moveDuration = 0.5f;
    [Tooltip("Animation of movement between tiles")]
    public AnimationCurve movementCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    // Timey wimey stuff
    public bool movingChangesTime = true;
    public Vector3 positionOfZeroTime = Vector3.zero;

    private bool isMoving;
    private float moveProgess = 1f;
    private Vector3 moveTargetPosition;
    private Vector3 moveSourcePosition;

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
                    moveTargetPosition = new Vector3(transform.position.x + Grid.singleton.tileSize * Mathf.Sign(movementIntent.x), transform.position.y, transform.position.z);
                }
                else if (movementIntent.z != 0)
                {
                    moveTargetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + Grid.singleton.tileSize * Mathf.Sign(movementIntent.z));
                }

                moveSourcePosition = transform.position;
                moveTargetPosition = Grid.singleton.GetSnappedPosition(moveTargetPosition);
                transform.rotation = Quaternion.LookRotation(moveTargetPosition - moveSourcePosition);

                if (movingChangesTime)
                {
                    // advance/reverse time
                    int targetTime = Mathf.RoundToInt(moveTargetPosition.x - positionOfZeroTime.x);
                    if (targetTime != TimeManager.singleton.currentTime)
                    {
                        Debug.Log($"Player advancing time by {targetTime - TimeManager.singleton.currentTime}");
                        TimeManager.singleton.StepTimeBy(targetTime - TimeManager.singleton.currentTime);
                    }
                }

                moveProgess = 0f;
                isMoving = true;
            }
        }
        else
        {
            // blend towards target position
            moveProgess += moveDuration > 0f ? Time.deltaTime / moveDuration : 1f - moveProgess;

            transform.position = Vector3.Lerp(moveSourcePosition, moveTargetPosition, movementCurve.Evaluate(moveProgess));

            if (moveProgess >= 1f)
            {
                transform.position = Grid.singleton.GetSnappedPosition(transform.position);
                isMoving = false;
            }
        }
    }
}
