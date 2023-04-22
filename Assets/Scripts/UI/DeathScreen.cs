using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject mainPanel;
    public TMPro.TextMeshProUGUI deathReason;

    private PlayerResponder playerResponder;

    private void Awake()
    {
        playerResponder = GetComponent<PlayerResponder>();
        playerResponder.onPlayerDied.AddListener(OnPlayerDied);
        mainPanel.SetActive(false);
    }

    private void OnPlayerDied(string reason)
    {
        deathReason.text = reason;

        mainPanel.SetActive(true);
    }
}
