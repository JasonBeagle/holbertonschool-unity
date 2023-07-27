using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject player;
    public GameObject timerCanvas;

    public void EndCutscene()
    {
        mainCamera.SetActive(true);
        player.GetComponent<PlayerController>().enabled = true;
        timerCanvas.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
