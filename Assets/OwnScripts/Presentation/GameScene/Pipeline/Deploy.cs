using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deploy : MonoBehaviour
{
    public Vector3 stopPosition;

    public Vector3 initialPosition;

    public GameObject[] clouds;

    public Color initialCloudColor;

    private bool descendPlatform;
    private bool reachedInitialPosition;

    private void Awake()
    {
        descendPlatform = false;
        reachedInitialPosition = false;
    }

    void Update()
    {
        if (descendPlatform)
        {
            Debug.Log($"Bajandooooo descendPlatform{descendPlatform}");
            if (!reachedInitialPosition)
            {
                Debug.Log("No se alcanzo position init");
                reachedInitialPosition = Mathf.Abs(gameObject.transform.position.y - initialPosition.y) <= 0.01;
                Debug.Log(gameObject.transform.position.y);
                Debug.Log(initialPosition.y);
                Debug.Log(reachedInitialPosition);
                Color randomColor = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                );
                foreach (GameObject cloud in clouds)
                {
                    cloud.GetComponent<MeshRenderer>().material.color = randomColor;
                }
                gameObject.transform.position += Vector3.down * Time.deltaTime * 2;

                if (reachedInitialPosition)
                {
                    descendPlatform = false;
                    foreach (GameObject cloud in clouds)
                    {
                        cloud.GetComponent<MeshRenderer>().material.color = initialCloudColor;
                    }
                }
            }           
        }        
    }

    private void OnTriggerStay(Collider other)
    {
        bool reachedFinalPosition = (int) gameObject.transform.position.y == (int) stopPosition.y;
        Debug.Log((int)gameObject.transform.position.y);
        if (other.CompareTag("Pickable"))
        {
            if (!reachedFinalPosition)
            {
                gameObject.transform.position += gameObject.transform.up * Time.deltaTime * 2;
            }
            else
            {
                Destroy(other.gameObject);
                descendPlatform = true;
            }
        }
    }

}
