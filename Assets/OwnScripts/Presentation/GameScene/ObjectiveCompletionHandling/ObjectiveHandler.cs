using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject target;

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Colison con player");
            SendObjectiveProgressed();
        } else if (other.CompareTag("Interactable")) {
            SendObjectiveProgressed();
        }
    }

    public void ObjectiveCompletedWhenAnimationEnds()
    {
        SendObjectiveProgressed();
    }
    
    private void SendObjectiveProgressed()
    {
        ExecuteEvents.Execute<IObjectiveSwitchHandler>(target, null, (x, y) => x.ObjectiveProgressed());
    }
}
