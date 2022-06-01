using System.Collections;
using UnityEngine;

public class RepositoryBallCounter : MonoBehaviour
{
    public GameObject pipelineManager;
    public int BallsIn { get; set; }
    private bool colliding;

    private void Awake()
    {
        colliding = false; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (colliding) return;
        colliding = true;
        if(other.CompareTag("Pickable"))
        {
            BallsIn++;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            Debug.Log("BallsIn : " + BallsIn);
            pipelineManager.GetComponent<PipelineExecution>().NotifyBallsNeeded(BallsIn);
        }
        StartCoroutine(Reset());
    }
    IEnumerator Reset()
    {
        yield return new WaitForEndOfFrame();
        colliding = false;
    }
}
