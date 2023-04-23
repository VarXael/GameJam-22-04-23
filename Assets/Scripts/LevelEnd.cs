using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private PlayerResponder playerResponder;
    private TimeResponder timeResponder;

    public TimeCondition unlockCondition = new TimeCondition();

    private bool isUnlocked = false;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onSteppedOnByPlayer.AddListener(OnSteppedOnByPlayer);
        timeResponder = GetComponent<TimeResponder>();
        timeResponder.onTimeChanged.AddListener(OnTimeChanged);

        isUnlocked = unlockCondition.IsTrue(TimeManager.singleton.currentTime);
    }

    private void OnSteppedOnByPlayer()
    {
        if (!Player.singleton.isDead)
            GameManager.singleton.FinishLevel();
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        isUnlocked = unlockCondition.IsTrue(newTime);

        foreach (var renderer in GetComponentsInChildren<Renderer>())
            renderer.enabled = isUnlocked;
    }
}
