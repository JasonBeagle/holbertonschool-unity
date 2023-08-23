using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 offset;
    private GameObject player;
    public float turnSpeed = 4.0f;
    public bool isInverted;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        player = GameObject.Find("Player");
        offset = transform.position - player.transform.position;

        if (PlayerPrefs.HasKey("InvertYToggle"))
        {
            isInverted = PlayerPrefs.GetInt("InvertYToggle") == 1;
        }
    }

    void LateUpdate()
    {
        float mouseY = isInverted ? -Input.GetAxis("Mouse Y") : Input.GetAxis("Mouse Y");

        offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * Quaternion.AngleAxis(mouseY * turnSpeed, Vector3.left) * offset;
        
        transform.position = player.transform.position + offset;
        
        transform.rotation = Quaternion.LookRotation(-offset, Vector3.up);
    }

    public void UpdateInverted()
    {
        if (PlayerPrefs.HasKey("InvertYToggle"))
            isInverted = PlayerPrefs.GetInt("InvertYToggle") == 1;
    }
}