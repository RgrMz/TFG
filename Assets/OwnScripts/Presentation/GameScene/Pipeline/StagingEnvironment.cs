using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagingEnvironment : MonoBehaviour
{
    public Vector3 positionToSpawn;

    IEnumerator GenerateBuild()
    {
        while(true)
        {
            GameObject build = Instantiate(Resources.Load($"OwnPrefabs/SoftwareBuildToBeDeployed"), positionToSpawn, transform.rotation) as GameObject;
            yield return new WaitForSeconds(120f);
        }
        
    }

    public void PeriodicallySpawnBuild()
    {
        StartCoroutine(GenerateBuild());
    }

}
