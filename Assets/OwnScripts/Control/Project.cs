using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Dictionary<string, double> Properties { get; set; }
    public Queue<Objective> Objectives { get; set; }
    public Objective CurrentObjective { get; set; }
    // Public List<Problem> Problems {get; set;} -> Generados al azar de entre todos los posibles
    
    public Project(int id,  string desc, string type)
    {
        Id = id;
        Description = desc;
        Type = type;
        Properties = new Dictionary<string, double>();
        Objectives = new Queue<Objective>();
        CurrentObjective = new Objective();
    }

    public Project()
    {
        Properties = new Dictionary<string, double>();
        Objectives = new Queue<Objective>();
        CurrentObjective = new Objective();
    }
}
