using System.Collections;
using System.Collections.Generic;

public class Objective
{
    private string Description { get; set; }
    private int NumberOfSteps { get; set; }
    private bool IsCompleted { get; set; }
    private string Type { get; set; }
    private bool TriggersPipeline { get; set; }
    private List<Effect> Effects { get; set; }

}
