using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveHandlerAnimations : MonoBehaviour, IEventSystemHandler
{
    private ObjectiveHandler handler;
    public GameObject target;

    public string CurrentObjectivePlace { get; private set; }

    private void Start()
    {
        handler = gameObject.AddComponent<ObjectiveHandler>();
    }
    public void SendObjectiveProgressedWhenAnimationEnds()
    {
        handler.SendObjectiveProgressed(target);
    }

    // Event sent from GameManager to know if a given interaction should make a progress in the current objective¡
    public void UpdateCurrentObjectivePlace(string currentObjectivePlace)
    {
        handler.UpdateCurrentObjectivePlace(currentObjectivePlace);
    }

    // Event sent from Zone Notification to know the place the player is
    public void UpdatePlayerPlace(string currentPlayerPlace)
    {
        handler.Place = currentPlayerPlace;
    }
}