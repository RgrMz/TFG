public class Effect
{
    public int EffectId {get; set;}
    public float Value { get; set; }
    public string Indicator { get; set; }

    public Effect(int id, float value, string indicator)
    {
        EffectId = id;
        Value = value;
        Indicator = indicator;
    }
}
