using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndScreen : MonoBehaviour
{
    public GameObject mainPanel;
    public Button continueButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinuePressed);
        GameManager.singleton.onPlayerFinishedLevel.AddListener(OnPlayerFinishedLevel);

        mainPanel.SetActive(false);
    }

    private void OnPlayerFinishedLevel()
    {
        mainPanel.SetActive(true);
    }

    private void OnContinuePressed()
    {
        mainPanel.SetActive(false);

        LevelManager.singleton.GoToNextLevel();
    }
}
