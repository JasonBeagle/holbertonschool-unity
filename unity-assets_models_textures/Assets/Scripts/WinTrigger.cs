using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;
    public int winTextSize = 60;
    public Color winTextColor = Color.green;

    void Start()
    {
        // get reference to the timer
        timer = GameObject.FindWithTag("Player").GetComponent<Timer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // stop the timer
            timer.started = false;
            // increase font size and change color to green
            timer.TimerText.fontSize = winTextSize;
            timer.TimerText.color = winTextColor;
        }
    }
}
