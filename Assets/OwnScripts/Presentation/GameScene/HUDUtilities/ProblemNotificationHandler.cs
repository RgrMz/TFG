using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProblemNotificationHandler : MonoBehaviour
{
    public GameObject ProblemNotification;

    public TextMeshProUGUI ProblemTextHUD;

    public GameObject ProblemNotificationBackground;

    public void SpawnOrDespawn(string text, float alpha)
    {
        ProblemTextHUD.text = text;

        Color problemNotiColor = ProblemNotification.GetComponent<Image>().color;
        Color problemNotiBackgroundColor = ProblemNotificationBackground.GetComponent<Image>().color;
        
        ProblemNotification.GetComponent<Image>().color = new Color(problemNotiColor.r, problemNotiColor.g, problemNotiColor.b, alpha);
        ProblemNotificationBackground.GetComponent<Image>().color = new Color(problemNotiBackgroundColor.r, 
            problemNotiBackgroundColor.g, problemNotiBackgroundColor.b, alpha/4);

    }

}
