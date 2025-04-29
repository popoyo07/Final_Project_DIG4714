using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float secondsCount;
    private int minuteCount;
    private int hourCount;

    void Update()
    {
        UpdateTimerUI();
    }

    // Call this on Update
    public void UpdateTimerUI()
    {
        // Accumulate time
        secondsCount += Time.deltaTime;

        // If seconds exceed 60, increment minutes and reset seconds
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount -= 60;
        }

        // If minutes exceed 60, increment hours and reset minutes
        if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }

        // Update UI
        timerText.text = hourCount + ":" + minuteCount.ToString("00") + ":" + ((int)secondsCount).ToString("00");

    }
}
