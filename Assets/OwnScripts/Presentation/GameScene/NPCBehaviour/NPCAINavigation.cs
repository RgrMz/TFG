using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAINavigation : MonoBehaviour
{
    // Lo mejor es tener un conjunto de Vector3 en el lado Dev y Ops y seleccioanr
    // randoms al inicio y para la vuelta igual
    // Y uno para coger bolas de la oficina y otro para cerca del repo y del ascensor
    public GameObject destination;
    public Vector3 originalPosition;
    NavMeshAgent npcAgent;
    private Animator npcAnim;

    private void Start()
    {
        npcAnim = GetComponent<Animator>();
        npcAgent = GetComponent<NavMeshAgent>();
        npcAgent.SetDestination(destination.transform.position);
        npcAnim.SetBool("Walk", true);
    }

    // Update is called once per frame
    void Update()
    {
        // npcAgent.SetDestination(destination.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcAgent.isStopped = false;
            npcAnim.SetBool("Walk", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            npcAgent.isStopped = true;
            npcAnim.SetBool("Walk", false);
        }

        if (other.CompareTag("NPCCheckpoint"))
        {
            Debug.Log("AAAAAA");
            npcAnim.SetBool("Walk", false);
            StartCoroutine(GoBack());
        }
    }

    // Once the destination is reached
    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(10f);
        Debug.Log("lego");
        // No funca, bsucar como resolver que setear una nueva destination no tire
        npcAgent.destination = originalPosition;
        
    }
}
