using System.Collections.Generic;
using UnityEngine;

public class IndicatorController
{
    public List<Indicator> Indicators { get; set; }

    public IndicatorController(Dictionary<string, float> projectProperties, string difficulty)
    {
        InitializeIndicators(projectProperties, difficulty);
    }

    private void InitializeIndicators(Dictionary<string, float> projectProperties, string difficulty)
    {
        Indicators = new List<Indicator>();
        List<string> indicatorProperties = new List<string>()
        { "CultureProgress", "AutomationProgress", "LeanProgress",
        "MeasurementProgress", "SharingProgress", "BudgetProgress",
        "DurationProgress" };
        float minValue, maxValue;
        if (difficulty.Equals("Basic"))
        {
            minValue = 60;
            maxValue = 70;
        }
        else
        {
            minValue = 50;
            maxValue = 60;
        }
        foreach (string name in indicatorProperties)
        {
            int index = indicatorProperties.IndexOf(name);
            int nameSeparator = name.IndexOf("P");
            if (name.Substring(0, nameSeparator).Equals("Duration"))
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                    projectProperties[indicatorProperties[index]], minValue, maxValue, projectProperties["InitialDuration"]));
            
            }
            else if (name.Substring(0, nameSeparator).Equals("Budget"))
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                    projectProperties[indicatorProperties[index]], minValue, maxValue, projectProperties["InitialBudget"]));
            }
            else
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                projectProperties[indicatorProperties[index]], minValue, maxValue));
            }
        }
        // Initialize the functionality bar independently
        Indicators.Add(new Indicator("Functionality", 0, 0, 0, 1));
    }

    public void NotifyIndicators()
    {
        foreach (Indicator indicator in Indicators)
        {
            indicator.update();
        }
    }
    public void NotifyIndicators(Effect effect)
    {
        foreach (Indicator indicator in Indicators)
        {
            indicator.update(effect);
        }
    }

}