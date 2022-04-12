using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ZoneNotificationTrigger : MonoBehaviour
{
    [SerializeField] private bool entryTrigger = false;
    [SerializeField] private bool exitTrigger = false;

    [SerializeField] private GameObject entryBlock = null;

    private GameObject[] enterTriggers = null;

    public TextMeshProUGUI zoneText;

    private GameObject zoneFrame = null;

    private void Start()
    {
        zoneFrame = GameObject.Find("ZoneFrame");
        enterTriggers = GameObject.FindGameObjectsWithTag("ZoneTrigger");
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject zoneTrigger in enterTriggers)
        {
            if (zoneTrigger.name.Equals(gameObject.name))
            {
                // Fade in effect, wait a few seconds and then fade out
                zoneText.text = zoneTrigger.name;
                StartCoroutine(FadeText(2f, zoneText, zoneFrame));

            }
        }
    }
    private void OnTriggerEnter (Collider other)
    {
        foreach (GameObject zoneTrigger in enterTriggers)
        {
            if (zoneTrigger.name.Equals(gameObject.name))
            {
                // Fade in effect, wait a few seconds and then fade out
                if(entryTrigger)
                    zoneText.text = zoneTrigger.name;
                StartCoroutine(FadeText(2f, zoneText, zoneFrame));

            }
        }
    }

    public IEnumerator FadeText(float t, TextMeshProUGUI text, GameObject frame)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        Image frameImage = zoneFrame.GetComponent<Image>();
        frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, 0);
        if(entryTrigger)
        {
            while (text.color.a < 1.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
                frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, frameImage.color.a + (Time.deltaTime / t));
                yield return null;
            }

            // Allows to exit from the building after 3s
            yield return new WaitForSeconds(1);
            entryBlock.GetComponent<MeshCollider>().enabled = false;
        }
        if(exitTrigger)
        {
            frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, frameImage.color.a);
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
            while (text.color.a > 0.0f)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
                frameImage.color = new Color(frameImage.color.r, frameImage.color.g, frameImage.color.b, frameImage.color.a - (Time.deltaTime / t));
                yield return null;
            }

            // On exit, block again the exit
            yield return new WaitForSeconds(3);
            entryBlock.GetComponent<MeshCollider>().enabled = true;
        }       
    }

}
