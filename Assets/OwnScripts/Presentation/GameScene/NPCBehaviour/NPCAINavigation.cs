using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAINavigation : MonoBehaviour
{
    // Lo mejor es tener un conjunto de Vector3 en el lado Dev y Ops y seleccioanr
    // randoms al inicio y para la vuelta igual
    // Y uno para coger bolas de la oficina y otro para cerca del repo y del ascensor
    public GameObject[] npcCheckpoints;

    NavMeshAgent npcAgent;
    private Animator npcAnim;
    private GameObject currentDestination;

    private static readonly System.Random random = new System.Random();
    private int randomCheckpoint;

    private void Start()
    {
        npcAnim = GetComponent<Animator>();
        npcAgent = GetComponent<NavMeshAgent>();
        npcAnim.SetBool("Walk", true);
        randomCheckpoint = random.Next(npcCheckpoints.Length - 1);
        currentDestination = npcCheckpoints[randomCheckpoint];
        npcAgent.SetDestination(currentDestination.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestinationReached();
    }


    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, currentDestination.transform.position);
        if (distanceToTarget < 0.5f)
        {
            ChooseRandomDestination();
            npcAgent.SetDestination(currentDestination.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ResumeWalking();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopWalking();
        }
    }
    
    internal void ChooseRandomDestination()
    {
        GameObject aux = currentDestination;
        do
        {
            randomCheckpoint = random.Next(npcCheckpoints.Length);
            currentDestination = npcCheckpoints[randomCheckpoint];
        } while (GameObject.ReferenceEquals(aux, currentDestination));
    }

    public void StopWalking()
    {
        npcAgent.isStopped = true;
        npcAnim.SetBool("Walk", false);
    }

    public void ResumeWalking()
    {
        npcAgent.isStopped = false;
        npcAnim.SetBool("Walk", true);
    }
}
