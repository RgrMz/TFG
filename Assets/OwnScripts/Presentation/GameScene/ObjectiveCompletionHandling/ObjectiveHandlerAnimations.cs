using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandlerAnimations : MonoBehaviour
{
    private ObjectiveHandler handler;
    public GameObject target;
    private void Start()
    {
        handler = gameObject.AddComponent<ObjectiveHandler>();
    }
    public void SendObjectiveProgressedWhenAnimationEnds()
    {
        handler.SendObjectiveProgressed(target);
    }
}
