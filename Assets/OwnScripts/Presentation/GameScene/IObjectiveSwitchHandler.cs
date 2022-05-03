using UnityEngine.EventSystems;

public interface IObjectiveSwitchHandler : IEventSystemHandler
{
    void ObjectiveCompleted();
    void RoleSelected(string role);
}
