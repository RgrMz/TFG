using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solution
{
    public int SolutionId { get; set; }
    public List<Effect> Effects { get; set; }
    public float Cost { get; set; }
    public float Profit { get; set; }
    public string Description { get; set; }

    public Solution (int id, float cost, float profit, string desc)
    {
        SolutionId = id;
        Cost = cost;
        Profit = profit;
        Description = desc;
    }
}
