using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Tooltip("Duration of a single move in seconds")]
    public float moveDuration = 0.5f;


    private bool isMoving;
    private float moveProgess = 1f;
    private Vector3 moveTargetPosition;

    void Update()
    {
        if (!isMoving)
        {
            // todo maybe don't do this every frame?
            transform.position = Grid.singleton.GetSnappedPosition(transform.position);

            // todo: might adjust based on camera if necessary
            Vector3 movementIntent = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (movementIntent.magnitude > 0f)
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

                moveProgess = 0f;
                isMoving = true;
            }
        }
        else
        {
        }
    }
}
