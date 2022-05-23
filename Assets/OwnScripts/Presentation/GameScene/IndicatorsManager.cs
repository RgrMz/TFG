using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorsManager : MonoBehaviour
{
    public GameObject gameManagerGO;

    protected GameManager gameManager;

    public List<GameObject> CALMSBars;

    public List<GameObject> ProjectBars;

    public Color originalCALMSIndicatorColor;
    public Color originalBudgetIndicatorColor;

    protected IndicatorController indicatorController;
    protected Problem currentProblem;
    protected bool indicatorsInitialized;
    protected const int GENERIC_PROBLEM_OBJECTIVE_ID = 4;
    protected Coroutine progressBarsCoroutine;
    void Start()
    {
        gameManagerGO = GameObject.Find("GameManager");
        gameManager = gameManagerGO.GetComponent<GameManager>();
    }

    public void InitializeIndicators()
    {
        indicatorController = new IndicatorController(gameManager.projectController.SelectedProject.Properties,
            gameManager.Difficulty);
        foreach (GameObject bar in CALMSBars)
        {
            Image barImage = bar.GetComponent<Image>();
            var indicator = from i in indicatorController.Indicators
                            where i.Name == bar.name.Substring(0, bar.name.IndexOf("B"))
                            select i;

            barImage.fillAmount = indicator.First<Indicator>().Value / 100;
        }
    }

    public void ApplyEffect(Effect effect)
    {
        indicatorController.NotifyIndicators(effect);
    }

    public Coroutine StartMakingBarsProgress(float initialBudget, float initialDuration)
    {
        progressBarsCoroutine = StartCoroutine(ApplyProgressToBars(initialBudget, initialDuration));
        return progressBarsCoroutine;
    }

    IEnumerator ApplyProgressToBars(float initialBudget, float initialDuration)
    {
        while (true)
        {
            // Change the values in the model
            indicatorController.NotifyIndicators();

            foreach (GameObject calmsBar in CALMSBars)
            {
                // Show the changes in the HUD for each bar
                int indexOfSeparator = calmsBar.name.IndexOf("B");
                Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals(calmsBar.name.Substring(0, indexOfSeparator)));
                calmsBar.GetComponent<Image>().fillAmount = (barIndicator.Value) / 100;
            }

            foreach (GameObject projectBar in ProjectBars)
            {
                // Show the changes in the HUD for each bar
                Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals(projectBar.name));
                if (barIndicator.Name.Equals("Budget"))
                {
                    projectBar.GetComponent<Image>().fillAmount = (barIndicator.Value) / initialBudget;
                }
                else if (barIndicator.Name.Equals("Duration"))
                {
                    projectBar.GetComponent<Image>().fillAmount = (barIndicator.Value) / initialDuration;
                }
            }

            // Apply the progress each second
            yield return new WaitForSeconds(1f);
        }
    }

    public void ApplyCostAndProfit(float cost, float profit)
    {
        Indicator budgetIndicator = indicatorController.Indicators.Find(
                   indicator => indicator.Name.Equals("Budget"));
        budgetIndicator.Value -= cost;
        budgetIndicator.Value += profit;
        GameObject budgetBar = ProjectBars.Find(bar => bar.name.Equals("Budget"));
        budgetBar.GetComponent<Image>().fillAmount = budgetIndicator.Value;
    }

    public void UpdateFunctionalityBar()
    {
        Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals("Functionality"));
        GameObject projectBar = ProjectBars.Find(bar => bar.name.Equals("Functionality"));

        barIndicator.Value++;

        projectBar.GetComponent<Image>().fillAmount = barIndicator.Value / (gameManager.projectController.SelectedProject.Objectives.Count - 3);
    }

    public List<Coroutine> BlinkIndicatorsAffected(int solutionNumber)
    {
        List<Coroutine> blinkCoroutines = new List<Coroutine>();
        currentProblem = gameManager.CurrentProblem;
        Solution solutionWatched = currentProblem.Solutions[solutionNumber - 1];
        foreach (Effect effect in solutionWatched.Effects)
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

        if (solutionWatched.Cost > 0)
        {
            blinkCoroutines.Add(StartCoroutine(BlinkNegative("Budget")));
        }
        else
        {
            blinkCoroutines.Add(StartCoroutine(BlinkPositive("Budget")));
        }

        return blinkCoroutines;
    }

    IEnumerator BlinkNegative(string indicatorName)
    {
        Image barImage = null;
        if (!indicatorName.Equals("Budget"))
        {
            foreach(GameObject bar in CALMSBars)
            {
                if (bar.name.Contains(indicatorName))
                {
                    barImage = bar.GetComponent<Image>();
                }
            }
        }
        else
        {
            barImage = ProjectBars[0].GetComponent<Image>();
        }
        
        Color negativeEffectColor = Color.red;
        for (int times = 0; times < 10; times++)
        {
            switch (barImage.color.a.ToString())
            {
                case "0":
                    barImage.color = negativeEffectColor;
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    barImage.color = new Color(barImage.color.r, barImage.color.g, barImage.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }

        barImage.color = originalCALMSIndicatorColor;
        yield break;

    }

    IEnumerator BlinkPositive(string indicatorName)
    {
        Image barImage = null;
        if (!indicatorName.Equals("Budget"))
        {
            foreach (GameObject bar in CALMSBars)
            {
                if (bar.name.Contains(indicatorName))
                {
                    barImage = bar.GetComponent<Image>();
                }
            }
        }
        else
        {
            barImage = ProjectBars[0].GetComponent<Image>();
        }

        Color negativeEffectColor = new Color(0.2f, 1, 0, 1);
        for (int times = 0; times < 10; times++)
        {
            switch (barImage.color.a.ToString())
            {
                case "0":
                    barImage.color = negativeEffectColor;
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    barImage.color = new Color(barImage.color.r, barImage.color.g, barImage.color.b, 0);
                    //Play sound
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
        }

        barImage.color = originalCALMSIndicatorColor;
        yield break;

    }
}