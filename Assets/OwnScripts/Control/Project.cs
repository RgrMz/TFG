using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Project
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public Dictionary<string, float> Properties { get; set; }
    public Queue<Objective> Objectives { get; set; }
    public Objective CurrentObjective { get; set; }
    public List<Problem> Problems {get; set;}
    public Project(int id,  string desc, string type)
    {
        Id = id;
        Description = desc;
        Type = type;
        Properties = new Dictionary<string, float>();
        Objectives = new Queue<Objective>();
        CurrentObjective = new Objective();
        Problems = new List<Problem>();
    }

    public Project()
    {
        Properties = new Dictionary<string, float>();
        Objectives = new Queue<Objective>();
        CurrentObjective = new Objective();
        Problems = new List<Problem>();
    }
}
