using System;
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
        if(gameObject.name.Equals("MovingPlatform"))
        {
            if (other.CompareTag("Pickable"))
            {
                SendObjectiveProgressed();
            }
        }
        else
        {
            if (!gameObject.name.Contains("Solution"))
            {
                if (other.CompareTag("Player") || other.CompareTag("Pickable"))
                {
                    SendObjectiveProgressed();
                }
            }
        }
    }

    public void SendObjectiveProgressed()
    {
        Debug.Log($"Place FOR CURENT COLLIDED ASSET IS {Place}");
        Debug.Log($"{Place}.Equals({CurrentObjectivePlace}) : {Place.Equals(CurrentObjectivePlace)}");
        if (Place != null)
        {
            if (Place.Equals(CurrentObjectivePlace) || Place.Equals("InitialZone"))
            {
                ExecuteEvents.Execute<IObjectiveSwitchHandler>(target, null, (gameManager, y) => gameManager.ObjectiveProgressed());
            }
        }
            
    }

    public void SendObjectiveProgressed(GameObject pTarget)
    {
        if (Place != null)
        {
            if (Place.Equals(CurrentObjectivePlace) || Place.Equals("InitialZone"))
            {
                ExecuteEvents.Execute<IObjectiveSwitchHandler>(pTarget, null, (gameManager, y) => gameManager.ObjectiveProgressed());
            }
        }
    }


    // Event sent from GameManager to know if a given interaction should make a progress in the current objective
    public void UpdateCurrentObjectivePlace(string currentObjectivePlace)
    {
        CurrentObjectivePlace = currentObjectivePlace;
        Debug.Log($"New place FOR CURRENT OBJECTIVE IS {CurrentObjectivePlace}");
    }

    public void SendSolutionChoose()
    {
        if (Place.Equals(CurrentObjectivePlace) || Place.Equals("InitialZone"))
        {
            ExecuteEvents.Execute<IObjectiveSwitchHandler>(target, null,
                            (manager, y) => manager.SolutionChoosed(gameObject.name));
        }
    }
}
