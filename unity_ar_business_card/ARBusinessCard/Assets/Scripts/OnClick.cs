using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class OnClick : MonoBehaviour
{
    public string url;
    public Animator animator;
    // private Vuforia.TrackableBehaviour TargetFound;


    void Start()
    {
        // TargetFound = GetComponentInParent<Vuforia.TrackableBehaviour>();
        // if (TargetFound)
        // {
        //     TargetFound.RegisterTrackableEventHandler(this);
        // }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                OnObjectClicked();
            }
        }
    }

    private void OnObjectClicked()
    {
        Debug.Log("clicked");
        Application.OpenURL(url);
    }

    private void OnTargetFound()
    {
        if (animator != null)
        {
            animator.SetTrigger("Found");
        }
    }

    private void OnTargetLost()
    {
        if (animator != null)
        {
            animator.SetTrigger("Lost");
        }
    }
}