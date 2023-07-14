using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public Timer timer;

    void Start()
    {
        // get reference to the timer
        timer = GameObject.FindWithTag("Player").GetComponent<Timer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered the TimerTrigger");
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player exited the TimerTrigger");
            // enable the timer and start counting
            timer.enabled = true;
            timer.StartTimer();
        }
    }
}
