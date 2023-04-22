using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject mainPanel;
    public TMPro.TextMeshProUGUI deathReason;

    public Button retryButton;
    public Button quitButton;

    private PlayerResponder playerResponder;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onPlayerDied.AddListener(OnPlayerDied);
        retryButton.onClick.AddListener(OnRetryPressed);
        quitButton.onClick.AddListener(OnQuitPressed);
        mainPanel.SetActive(false);
    }

    private void OnPlayerDied(string reason)
    {
        deathReason.text = reason;

        mainPanel.SetActive(true);
    }

    private void OnRetryPressed()
    {
        mainPanel.SetActive(false);
        LevelManager.singleton.RestartLevel();
    }

    private void OnQuitPressed()
    {
        mainPanel.SetActive(false);
        Application.Quit();
    }
}
