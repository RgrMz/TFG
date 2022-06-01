using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPhase : MonoBehaviour
{
    private int numberOfBallsArrived;
    public int BallsNeeded { get; set; }
    private static Vector3 buildPosition;
    private bool buildDone;
    private bool isColliding;

    private void Awake()
    {
        // Position taken from the Unity Editor
        buildPosition = new Vector3(124.050003f, 1.67999995f, 24.5f);
        numberOfBallsArrived = 0;
        BallsNeeded = 1;
        buildDone = false;
    }
    private void Update()
    {
        Debug.Log("BallsNeeded : " + BallsNeeded);
        isColliding = false;
        if (!buildDone)
        {
            if (numberOfBallsArrived == BallsNeeded)
            {
                buildDone = true;
                GenerateBuild();
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;
        if (other.CompareTag("Pickable"))
        {
            numberOfBallsArrived++;
            buildDone = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            Destroy(other.gameObject);
        }
    }

    public void GenerateBuild()
    {
        GameObject build = Instantiate(Resources.Load($"OwnPrefabs/SoftwareBuildToBeDeployed"), buildPosition, transform.rotation) as GameObject;
        numberOfBallsArrived = 0;
    }
}
