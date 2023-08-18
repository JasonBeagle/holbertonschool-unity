using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    private float startTime;
    public bool started;
    public TMPro.TextMeshProUGUI FinalTime;

    void Update()
    {
        if (started)
        {
            float t = Time.time - startTime;

            string minutes = ((int) t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            TimerText.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        started = true;
    }

    public void ResetTimer()
    {
        TimerText.text = "0:00.00";
        started = false;
    }

    // New Win method
    public void Win()
    {
        if (FinalTime != null)
        {
            FinalTime.text = TimerText.text; // Set the FinalTime text to the current TimerText
        }
    }
}
