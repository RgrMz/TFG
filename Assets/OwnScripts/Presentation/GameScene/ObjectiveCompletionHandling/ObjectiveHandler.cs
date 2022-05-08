using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveHandler : MonoBehaviour, IEventSystemHandler
{
    public GameObject target;

    [SerializeField]
    private string place;
    public string Place
    {
        get { return place; }
        set { place = value; }
    }

    public string CurrentObjectivePlace { get; set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Pickable"))
        {
            if (Place.Equals("InitialZone"))
                SendObjectiveProgressed();
            if (Place.Equals(CurrentObjectivePlace))
                SendObjectiveProgressed();
        }
    }

    public void SendObjectiveProgressed(GameObject pTarget = null)
    {
        ExecuteEvents.Execute<IObjectiveSwitchHandler>(pTarget == null ? target : pTarget, null, (x, y) => x.ObjectiveProgressed());
    }

    // Event sent from GameManager to know if a given interaction should make a progress in the current objective
    public void UpdateCurrentObjectivePlace(string currentObjectivePlace)
    {
        CurrentObjectivePlace = currentObjectivePlace;
        Debug.Log($"New place is {currentObjectivePlace}");
    }
}
