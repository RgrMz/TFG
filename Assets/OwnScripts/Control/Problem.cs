using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Problem
{
    public int ProblemId { get; set; }
    public List<Solution> Solutions { get; set; }
    public int NumberOfSolutions { get; set; }
    public string Description { get; set; }

    public Problem(int problemId, string desc, List<Solution> solutions)
    {
        ProblemId = problemId;
        Description = desc;
        Solutions = solutions;
        NumberOfSolutions = solutions.Count;
    }
}
