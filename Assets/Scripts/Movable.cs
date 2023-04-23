using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour
{
    [Tooltip("Duration of a single move in seconds")]
    public float moveDuration = 0.5f;
    [Tooltip("Animation of movement between tiles")]
    public AnimationCurve movementCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public bool isMoving { get; private set; } = false;
    private float moveProgess = 1f;
    private Vector3 moveTargetPosition;
    private Vector3 moveSourcePosition;

    public Vector3 effectivePosition => isMoving ? moveTargetPosition : transform.position;

    private void Awake()
    {
        moveTargetPosition = transform.position;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        moveSourcePosition = transform.position;
        moveTargetPosition = LevelManager.singleton.GetSnappedPosition(targetPosition);

        moveProgess = 0f;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
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
}
