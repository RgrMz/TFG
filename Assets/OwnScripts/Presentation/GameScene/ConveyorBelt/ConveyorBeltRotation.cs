using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltRotation : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> objectsOnBelt;

    // Update is called once per frame
    void Update()
    {
        for(int i=0; i <= objectsOnBelt.Count - 1; i++)
        {
            objectsOnBelt[i].GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectsOnBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        objectsOnBelt.Remove(collision.gameObject);
    }
}
