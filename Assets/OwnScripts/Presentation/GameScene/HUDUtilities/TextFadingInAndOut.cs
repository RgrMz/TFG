using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextFadingInAndOut : MonoBehaviour
{     
    public static IEnumerator FadeText(GameObject background, float t, TextMeshProUGUI text)
    {
        if(text.text.Equals("") || text.text == null)
        {
            text.text = "Objective completed!";
        }
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
