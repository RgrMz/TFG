using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private Animator door = null;

    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    [SerializeField] private string openAnimation = "DoorOpen";
    [SerializeField] private string closeAnimation = "DoorClose";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                door.Play(openAnimation, 0, 0.0f);
                gameObject.SetActive(false);
            }
            else if (closeTrigger)
            {
                door.Play(closeAnimation, 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
