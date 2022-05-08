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

    protected IndicatorController indicatorController;
    protected bool indicatorsInitialized;
    protected Coroutine progressBarsCoroutine;
    void Start()
    {
        gameManagerGO = GameObject.Find("GameManager");
        gameManager = gameManagerGO.GetComponent<GameManager>();
    }

    void Update()
    {

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

    public void StartMakingBarsProgress()
    {
        progressBarsCoroutine = StartCoroutine(ApplyProgressToBars());
    }

    IEnumerator ApplyProgressToBars()
    {
        while (true)
        {
            foreach (GameObject calmsBar in CALMSBars)
            {
                // Change the values in the model
                indicatorController.NotifyIndicators();

                // Show the changes in the HUD for each bar
                int indexOfSeparator = calmsBar.name.IndexOf("B");
                Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals(calmsBar.name.Substring(0, indexOfSeparator)));
                calmsBar.GetComponent<Image>().fillAmount += (barIndicator.ProgressPerSecond) / 100;
            }
            foreach (GameObject projectBar in ProjectBars)
            {
                // Change the values in the model
                indicatorController.NotifyIndicators();

                // Show the changes in the HUD for each bar
                Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals(projectBar.name));
                projectBar.GetComponent<Image>().fillAmount += (barIndicator.ProgressPerSecond) / 100;
            }

            // Apply the progress each 4 seconds
            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateFunctionalityBar()
    {
        Indicator barIndicator = indicatorController.Indicators.Find(
                    indicator => indicator.Name.Equals("Functionality"));
        GameObject projectBar = ProjectBars.Find(bar => bar.name.Equals("Functionality"));

        // Update a constant value based on the number of objectives of the project
        // Substract for 2 objectives: the two initial ones and the last one
        barIndicator.Value += 1 / (gameManager.projectController.SelectedProject.Objectives.Count - 3);
        projectBar.GetComponent<Image>().fillAmount = barIndicator.Value;
    }
}