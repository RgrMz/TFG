using System.Collections;
using UnityEngine;

public class PipelineExecution : MonoBehaviour
{
    public GameObject pipelineDoor;

    public bool Trigger { get; set; }
    private Vector3 initialPipelineDoorPosition;

    // Start is called before the first frame update
    void Awake()
    {
        Trigger = false;
        initialPipelineDoorPosition = pipelineDoor.transform.position;
    }

    public void StartExecution()
    {
        StartCoroutine(OpenPipelineDoor());
    }

    IEnumerator OpenPipelineDoor()
    {

        float y = initialPipelineDoorPosition.y;

        while (pipelineDoor.transform.position.y < initialPipelineDoorPosition.y + 3)
        {
            y += 0.1f * Time.deltaTime * 2;
            pipelineDoor.transform.position = new Vector3(initialPipelineDoorPosition.x, y, initialPipelineDoorPosition.z);
            yield return null;
        }

        yield return new WaitForSeconds(4);

        y = pipelineDoor.transform.position.y;
        while (pipelineDoor.transform.position.y > initialPipelineDoorPosition.y)
        {
            y -= 0.1f * Time.deltaTime;
            pipelineDoor.transform.position = new Vector3(initialPipelineDoorPosition.x, y, initialPipelineDoorPosition.z);
            yield return null;
        }

        yield break;
    }
}
