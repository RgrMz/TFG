using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveHandler : MonoBehaviour
{
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pickable"))
        {
            Debug.Log("Colison con player o pickable");
            SendObjectiveProgressed();
        }
    }

    public void SendObjectiveProgressed(GameObject pTarget = null)
    {
        ExecuteEvents.Execute<IObjectiveSwitchHandler>(pTarget == null ? target : pTarget, null, (x, y) => x.ObjectiveProgressed());
    }
}
