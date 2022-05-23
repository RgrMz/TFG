using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingIndicatorsEffects : MonoBehaviour
{
    public GameObject gameManagerGO;
    private Objective currentObjective;
    private const int GENERIC_PROBLEM_OBJECTIVE_ID = 4;
    // Start is called before the first frame update
    void Start()
    {
        currentObjective = gameManagerGO.GetComponent<GameManager>().projectController.SelectedProject.CurrentObjective;
    }

    public List<Coroutine> BlinkIndicatorsAffected()
    {
        List<Coroutine>  blinkCoroutines = new List<Coroutine>();
        if (currentObjective.ObjectiveId == GENERIC_PROBLEM_OBJECTIVE_ID)
        {
            foreach (Effect effect in currentObjective.Effects)
            {
                if (effect.Value < 0)
                {
                    blinkCoroutines.Add(StartCoroutine(BlinkNegative(effect.Indicator)));
                }
                else
                {
                    blinkCoroutines.Add(StartCoroutine(BlinkPositive(effect.Indicator)));
                }
            }
        }

        return blinkCoroutines;
    }

    IEnumerator BlinkNegative(object indicatorName)
    {
        throw new NotImplementedException();
    }

    IEnumerator BlinkPositive(object indicatorName)
    {
        throw new NotImplementedException();
    }

    //IEnumerator BlinkIndicators()
    //{
    //    //Image
    //    //// Just in case, check that the current objective is a problem
    //    //if (currentObjective.ObjectiveId == GENERIC_PROBLEM_OBJECTIVE_ID)
    //    //{
    //    //    while (true)
    //    //    {
    //    //        switch (image.color.a.ToString())
    //    //        {
    //    //            case "0":
    //    //                image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    //    //                //Play sound
    //    //                yield return new WaitForSeconds(0.5f);
    //    //                break;
    //    //            case "1":
    //    //                image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    //    //                //Play sound
    //    //                yield return new WaitForSeconds(0.5f);
    //    //                break;
    //    //        }
    //    //    }
    //    //}
    //}
}
