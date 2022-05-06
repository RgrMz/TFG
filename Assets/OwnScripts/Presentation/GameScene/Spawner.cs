using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Some data needed to calculate if a bugged ball should be spawned based on practices & CALMS
    // Starting probability = 0.3 maybe, for each bug decrease it since you learn about it

    // Gather the objective in which the player is to be able to know which ball to spawn
    private string objectiveType;

    public TextMeshProUGUI finishedInteractionText;

    private GameObject ballToSpawn;

    private Vector3 positionToSpawn;

    private Animator anim;

    public GameObject background;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void spawnBall()
    {
        // zoneName = event.text;
        objectiveType = "Java";
        positionToSpawn = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z - 2);
        switch (objectiveType)
        {
            case "Java":
                ballToSpawn = Instantiate(Resources.Load("OwnPrefabs/JavaBall"), positionToSpawn, transform.rotation) as GameObject;
                break;
        }
    }

}
