using UnityEngine.EventSystems;

public interface IObjectiveSwitchHandler : IEventSystemHandler
{
    void ObjectiveProgressed();
    void RoleSelected(string role);
}
