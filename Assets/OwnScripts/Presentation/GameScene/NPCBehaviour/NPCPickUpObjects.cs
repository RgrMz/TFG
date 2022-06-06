using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCPickUpObjects : MonoBehaviour
{
    public GameObject npcPickUpParent;

    private Animator npcAnim;

    public GameObject officeWaitingSpot;

    public GameObject destination;

    public string originalNpcTag;

    public GameObject spawnPosition;

    public string ballType;

    private GameObject ballToSpawn;

    private bool isBallSpawned;

    private void Start()
    {
        originalNpcTag = gameObject.tag;
        npcAnim = GetComponent<Animator>();
        isBallSpawned = false;
        StartCoroutine(SpawnBall());
    }

    private void Update()
    {
        if (isBallSpawned)
        {
            GetComponent<NavMeshAgent>().SetDestination(ballToSpawn.transform.position);
            npcAnim.SetBool("Walk", true);
            isBallSpawned = false;
        }
        CheckDestinationReached();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            npcAnim.SetBool("Walk", false);
            npcAnim.SetTrigger("PickUp");
            StartCoroutine(other.gameObject.GetComponent<InteractableItemBase>().AttachBall(npcPickUpParent));
            gameObject.GetComponent<NavMeshAgent>().destination = destination.transform.position;
            npcAnim.SetBool("Walk", true);
            gameObject.tag = "PickingObject";
        }
    }

    void CheckDestinationReached()
    {
        float distanceToTarget = Vector3.Distance(transform.position, destination.transform.position);
        if (distanceToTarget < 0.5f)
        {
            npcAnim.SetBool("Walk", false);
            npcAnim.SetTrigger("Throw");
            StartCoroutine(npcPickUpParent.transform.GetChild(0).GetComponent<InteractableItemBase>().DetachBall(npcPickUpParent));
            npcAnim.SetBool("Walk", true);
            gameObject.GetComponent<NavMeshAgent>().destination = officeWaitingSpot.transform.position;
            gameObject.tag = originalNpcTag;
        }
    }

    IEnumerator SpawnBall()
    {
        while (true)
        {
            
            ballToSpawn = Instantiate(Resources.Load($"OwnPrefabs/{ballType}Ball"), spawnPosition.transform.position, transform.rotation) as GameObject;
            isBallSpawned = true;
            yield return new WaitForSeconds(60f);
        }
    }
}
