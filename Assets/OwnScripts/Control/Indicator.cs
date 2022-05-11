using System;

public class Indicator : Subscriber
{
    public float Value { get; set; }
    public string Name { get; set; }
    public float ProgressPerSecond { get; set; }
    private float originalProgressPerSecond;
    public float[] MinMaxValueInitialization { get; set; }
    private static readonly Random random = new Random();

    public Indicator(string name, float progressPerSec, float minValue, float maxValue, float initialValue)
    {
        Name = name;
        ProgressPerSecond = progressPerSec;
        originalProgressPerSecond = progressPerSec;
        MinMaxValueInitialization = new float[] { minValue, maxValue };
        Value = initialValue;
    }

    public Indicator(string name, float progressPerSec, float minValue, float maxValue)
    {
        Name = name;
        ProgressPerSecond = progressPerSec;
        originalProgressPerSecond = progressPerSec;
        MinMaxValueInitialization = new float[] { minValue, maxValue };
        Value = (float)(random.NextDouble() * (maxValue - minValue) + minValue);
    }

    public void update(Effect effect = null)
    {
        if (!Name.Equals("Functionality"))
        {
            if (effect != null)
            {
                if (effect.Indicator.Equals(Name))
                {
                    Value += effect.Value;
                }
            }
            else
            {
                // If there's no effect to apply, update the value acording to the progress
                Value += ProgressPerSecond;
            }
        }
    }

    public void restoreOriginalProgressAfterEffect()
    {
        ProgressPerSecond = originalProgressPerSecond;
    }
}