using System;
using UnityEngine;

[Serializable]
public class Indicator : Subscriber
{
    [SerializeField]
    public float _value;
    public float Value { get { return _value; } set { _value = value; } }

    [SerializeField]
    public string name;
    public string Name { get { return name; } set { name = value; } }
    public float ProgressPerSecond { get; set; }
    private float originalProgressPerSecond;
    public float[] MinMaxValueInitialization { get; set; }
    private static readonly System.Random random = new System.Random();

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