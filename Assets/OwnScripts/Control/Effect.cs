public class Effect
{
    public int EffectId {get; set;}
    public double Value { get; set; }
    public string Indicator { get; set; }
    public double Duration { get; set; }

    public Effect(int id, double value, string indicator, double duration)
    {
        EffectId = id;
        Value = value;
        Indicator = indicator;
        Duration = duration;
    }
}
