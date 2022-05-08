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
        Debug.Log($"Player in {gameObject.name}");
        ExecuteEvents.Execute<ObjectiveHandlerAnimations>(player, null, (x, y) => x.UpdatePlayerPlace(gameObject.name));
    }
    private void OnTriggerExit(Collider other)
    {
        ExecuteEvents.Execute<ObjectiveHandlerAnimations>(player, null, (x, y) => x.UpdatePlayerPlace(""));
    }
}
