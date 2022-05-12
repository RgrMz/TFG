using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoleSelectionHandler : MonoBehaviour
{
    public GameObject target;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            string role = gameObject.name.Contains("Dev") ? "Dev" : "Ops";
            ExecuteEvents.Execute<IObjectiveSwitchHandler>(target, null, (x, y) => x.RoleSelected(role));
        }
    }
}
