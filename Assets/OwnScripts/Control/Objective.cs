using System.Collections;
using System.Collections.Generic;

public class Objective
{
    public int ObjectiveId { get; set; }
    public string Description { get; set; }
    public int NumberOfSteps { get; set; }
    public bool IsCompleted { get; set; }
    public string Type { get; set; }
    public bool TriggersPipeline { get; set; }
    public int Order { get; set; }
    public string Place { get; set; }
    public int CurrentStep { get; set; }
    public List<Effect> Effects { get; set; }
    public Objective()
    {
        IsCompleted = false;
    }
    public Objective (int id, string desc, int numberOfSteps, bool isCompleted, string type, bool triggers, int order, string place)
    {
        ObjectiveId = id;
        Description = desc;
        NumberOfSteps = numberOfSteps;
        IsCompleted = isCompleted;
        Type = type;
        IsCompleted = isCompleted;
        TriggersPipeline = triggers;
        Order = order;
        Place = place;
        CurrentStep = 1;
    }

    public Objective(int id, string desc, int numberOfSteps, bool isCompleted, string type, bool triggers, int order, string place, List<Effect> effects)
    {
        ObjectiveId = id;
        Description = desc;
        NumberOfSteps = numberOfSteps;
        IsCompleted = isCompleted;
        Type = type;
        TriggersPipeline = triggers;
        Effects = effects;
        Order = order;
        Place = place;
        CurrentStep = 1;
    }

}
