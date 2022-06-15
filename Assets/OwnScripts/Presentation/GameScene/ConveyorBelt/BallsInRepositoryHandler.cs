using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsInRepositoryHandler : MonoBehaviour
{
    public GameObject pipelineManager;
    private int BallsIn { get; set; }

    private void Start()
    {
        BallsIn = 0;
    }

    private void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Pickable"))
        {
            BallsIn++;
            pipelineManager.GetComponent<PipelineExecution>().NotifyBallsNeeded(BallsIn);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        BallsIn--;
    }
}
