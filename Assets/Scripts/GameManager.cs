using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onPlayerDied.AddListener(OnPlayerDied);
    }

    private void OnPlayerDied(string reason)
    {
        // Im not sure what Im doing
    }
}
