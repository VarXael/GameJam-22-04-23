using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaserGuyCountdownUI : MonoBehaviour
{
    private LaserGuy laserGuy;

    public TextMeshProUGUI countdownText;
    public Image countdownImage;

    private void Awake()
    {
        laserGuy = GetComponentInParent<LaserGuy>();
        laserGuy.onTick.AddListener(OnLaserGuyTick);
    }

    private void OnLaserGuyTick(int timeRemaining, int maxTime)
    {
        if (countdownText)
            countdownText.text = timeRemaining.ToString();
        if (countdownImage)
            countdownImage.fillAmount = maxTime > 0 ? (float)timeRemaining / (float)(maxTime) : 1f;
    }
}
