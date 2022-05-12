using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZoneNotificationForPlayer : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // Debug.Log($"Player in {gameObject.name}");
            ExecuteEvents.Execute<ObjectiveHandlerAnimations>(player, null, (x, y) => x.UpdatePlayerPlace(gameObject.name));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Debug.Log($"Player exited");
            ExecuteEvents.Execute<ObjectiveHandlerAnimations>(player, null, (x, y) => x.UpdatePlayerPlace(""));
        }
    }
}
