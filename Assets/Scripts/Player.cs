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

    // Timey wimey stuff
    public bool movingChangesTime = true;
    public Vector3 positionOfZeroTime = Vector3.zero;

    public bool isDead { get; private set; }

    public Vector3 effectivePosition => movable.effectivePosition;

    private Movable movable;

    private void Awake()
    {
        movable = GetComponent<Movable>();
    }

    void Update()
    {
        if (!movable.isMoving)
        {
            // todo maybe don't do this every frame?

            // todo: might adjust based on camera if necessary
            Vector3 movementIntent = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

            // decide whether to move
            if (movementIntent.x != 0 || movementIntent.z != 0) // if user is pressing diagonally don't move them at all I guess
            {
                // hack cus FindObjectsOfType is bad: enact player movement responders now
                PlayerResponder[] playerResponders = FindObjectsOfType<PlayerResponder>();
                Vector3 targetPosition = default; // guaranteed to be non-zero after the next checks
                int desiredTimeOffset = 0;

                // hmm I guess since this is a tile-based game we only want to move one axis at a time (new to me)
                if (movementIntent.x != 0)
                {
                    targetPosition = new Vector3(transform.position.x + LevelManager.singleton.tileSize * Mathf.Sign(movementIntent.x), transform.position.y, transform.position.z);
                    desiredTimeOffset = Mathf.RoundToInt(Mathf.Sign(movementIntent.x));
                }
                else if (movementIntent.z != 0)
                {
                    targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + LevelManager.singleton.tileSize * Mathf.Sign(movementIntent.z));
                }

                // Check with player responders that we can move in this direction
                bool hasBeenBlocked = false;
                foreach (var playerResponder in playerResponders)
                {
                    if (playerResponder.blocksPlayer && playerResponder.DoesOccupyPosition(targetPosition))
                    {
                        hasBeenBlocked = true;
                    }
                }

                // turn towards the tile even if we were blocked
                transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);

                if (!hasBeenBlocked)
                {
                    movable.MoveTo(targetPosition);
                    
                    if (movingChangesTime)
                    {
                        // advance/reverse time
                        if (desiredTimeOffset != 0)
                        {
                            TimeManager.singleton.StepTimeBy(desiredTimeOffset);
                        }
                    }

                    // tell player responders we've moved
                    foreach (var movementResponder in playerResponders)
                        movementResponder.onPlayerMoved?.Invoke(1);

                    LevelManager.singleton.OnPlayerSteppedTo(targetPosition);
                }
                else
                {
                    Debug.Log("can't move here due to blocking object");
                }
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
