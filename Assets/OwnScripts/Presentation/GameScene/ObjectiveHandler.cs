using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject target;

    public void OnTriggerEnter (Collider other)
    {
        if (other.tag.Equals("Player"))
        {            
            ExecuteEvents.Execute<IObjectiveSwitchHandler>(target, null, (x, y) => x.ObjectiveCompleted());
        }
    }
}
