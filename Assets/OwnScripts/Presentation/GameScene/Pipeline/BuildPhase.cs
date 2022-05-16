using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPhase : MonoBehaviour
{
    private int numberOfBallsArrived;
    public int BallsNeeded { get; set; }
    private static Vector3 buildPosition;
    private void Awake()
    {
        // Position taken from the Unity Editor
        buildPosition = new Vector3(123.050003f, 1.67999995f, 24.5f);
        numberOfBallsArrived = 0;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            numberOfBallsArrived++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickable"))
        {
            Destroy(other.gameObject);
        }
    }

    public void GenerateBuild()
    {
        if (numberOfBallsArrived == BallsNeeded)
        {
            GameObject build = Instantiate(Resources.Load($"OwnPrefabs/SoftwareBuildToBeDeployed"), buildPosition, transform.rotation) as GameObject;
        }
    }
}
