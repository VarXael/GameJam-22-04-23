using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderVisualiser : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        float newSliderValue = TimeManager.singleton.currentTime;

        if (newSliderValue != slider.value)
        {
            slider.value = newSliderValue;
        }

        // note we wouldn't normally do this every frame but GAME JAM
        if (TimeManager.singleton.minTime != slider.minValue)
            slider.minValue = TimeManager.singleton.minTime;
        if (TimeManager.singleton.maxTime != slider.maxValue)
            slider.maxValue = TimeManager.singleton.maxTime;
    }
}
