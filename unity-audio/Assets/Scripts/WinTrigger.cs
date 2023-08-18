using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Timer timer;
    public int winTextSize = 60;
    public Color winTextColor = Color.green;
    public float delayBeforeReset = 5f; // delay in seconds

    // Declare a public GameObject for the WinCanvas
    public GameObject winCanvas;

    void Start()
    {
        // get reference to the timer
        timer = GameObject.FindWithTag("Player").GetComponent<Timer>();
        // Ensure the WinCanvas is initially inactive
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer.Win();
            // stop the timer
            timer.started = false;
            // increase font size and change color to green
            timer.TimerText.fontSize = winTextSize;
            timer.TimerText.color = winTextColor;

            // start the coroutine to delay reset
            StartCoroutine(DelayedReset());

            // activate the WinCanvas
            if (winCanvas != null)
            {
                winCanvas.SetActive(true);
            }

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
    }

    IEnumerator DelayedReset()
    {
        timer.Win();
        
        // wait for the specified delay
        yield return new WaitForSeconds(delayBeforeReset);

        // reset the timer
        timer.ResetTimer();

        // reset the timer text to its initial state
        timer.TimerText.fontSize = 48;  // assuming initial font size is 14
        timer.TimerText.color = Color.white;  // assuming initial color is white

        // deactivate the WinCanvas
        if (winCanvas != null)
        {
            winCanvas.SetActive(false);
        }
    }
}
