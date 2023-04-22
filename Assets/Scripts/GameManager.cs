using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = FindObjectOfType<GameManager>();
            }

            return _singleton;
        }
    }
    private static GameManager _singleton;

    private PlayerResponder playerResponder;

    public UnityEvent onPlayerFinishedLevel;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onPlayerDied.AddListener(OnPlayerDied);
    }

    public void FinishLevel()
    {
        onPlayerFinishedLevel?.Invoke();
    }

    private void OnPlayerDied(string reason)
    {
        // Im not sure what Im doing
    }
}
