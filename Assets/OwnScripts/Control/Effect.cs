public class Effect
{
    private double Value { get; set; }
    private string Indicator { get; set; }
    private double Duration { get; set; }

    public Effect(double value, string indicator, double duration)
    {
        Value = value;
        Indicator = indicator;
        Duration = duration;
    }
}
