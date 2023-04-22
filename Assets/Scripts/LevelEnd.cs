using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private PlayerResponder playerResponder;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onSteppedOnByPlayer.AddListener(OnSteppedOnByPlayer);
    }

    private void OnSteppedOnByPlayer()
    {
        GameManager.singleton.FinishLevel();
    }
}
