using System;

public class Indicator : Subscriber
{
    public float Value { get; set; }
    public string Name { get; set; }
    public float ProgressPerSecond { get; set; }
    public float[] MinMaxValueInitialization { get; set; }
    private static readonly Random random = new Random();

    public Indicator(string name, float progressPerSec, float minValue, float maxValue, float initialValue = 0)
    {
        Name = name;
        ProgressPerSecond = progressPerSec;
        MinMaxValueInitialization = new float[] { minValue, maxValue };
        if (name.Equals("Duration") || name.Equals("Budget"))
        {
            Value = initialValue;
        }
        if (initialValue != 0)
        {
            Value = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
        }
    }

    public void update(Effect effect = null)
    {
        if (!Name.Equals("Functionality"))
        {
            if (effect != null)
            {
                if (effect.Indicator.Equals(Name))
                {
                    ProgressPerSecond = effect.Value;
                }
            }
            else
            {
                // If there's no effect to apply, update the value acording to the progress
                Value += ProgressPerSecond;
            }
        }
    }

}