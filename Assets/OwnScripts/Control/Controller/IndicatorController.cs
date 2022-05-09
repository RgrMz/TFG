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
            minValue = 40;
            maxValue = 50;
        }
        else
        {
            minValue = 30;
            maxValue = 40;
        }
        foreach (string name in indicatorProperties)
        {
            int i = 0;
            int nameSeparator = name.IndexOf("P");
            if (name.Equals("Duration"))
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                    projectProperties[indicatorProperties[i]], minValue, maxValue, projectProperties["InitialDuration"]));
            }
            else if (name.Equals("Budget"))
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                    projectProperties[indicatorProperties[i]], minValue, maxValue, projectProperties["InitialBudget"]));
            }
            else
            {
                Indicators.Add(new Indicator(name.Substring(0, nameSeparator),
                projectProperties[indicatorProperties[i]], minValue, maxValue));
            }
            i++;
            
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