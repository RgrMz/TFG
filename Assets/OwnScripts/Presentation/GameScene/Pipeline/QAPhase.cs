using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QAPhase : MonoBehaviour
{
    public Light qaLight;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Pickable"))
        {
            StartCoroutine(CheckBallsAreNotBugged(other));
        }
    }

    IEnumerator CheckBallsAreNotBugged(Collider other)
    {
        Material ballType = other.GetComponent<MeshRenderer>().material;
        if(!ballType.name.Equals("Bug"))
        {
            yield return new WaitForSeconds(5f);
            qaLight.intensity = 20;
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(5f);
            gameObject.GetComponent<Rigidbody>().detectCollisions = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            qaLight.intensity = 0;
        }
        yield break;
    }

}
