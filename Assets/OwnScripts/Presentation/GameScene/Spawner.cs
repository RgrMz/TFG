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
    public void spawnFinishedInteraction()
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals("Typing"))
            {
                finishedInteractionText.text = "Work finished!";
            }
        }
        StartCoroutine(FadeText(2f, finishedInteractionText));
    }

    public IEnumerator FadeText(float t, TextMeshProUGUI text)
    {
        Image backgroundImage = background.GetComponent<Image>();
        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        while (text.color.a < 1.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / t));
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundImage.color.a + (Time.deltaTime / t));
            yield return null;
        }

        // Fade out after 3 seconds
        yield return new WaitForSeconds(2);

        backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundImage.color.a);
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / t));
            backgroundImage.color = new Color(backgroundImage.color.r, backgroundImage.color.g, backgroundImage.color.b, backgroundImage.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}