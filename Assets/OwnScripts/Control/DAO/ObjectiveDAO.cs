﻿using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using Assets.Scripts.Persistance;
using System;
using UnityEngine;
public class ObjectiveDAO
{
    private Singleton db;
    private string sqlQuery;
    public ObjectiveDAO()
    {
        db = Singleton.GetInstance();
        sqlQuery = null;
    }

    public Queue<Objective> GetAllObjectives (int projectId)
    {
        Queue<Objective> objectives = new Queue<Objective>();
        
        string sqlQuery2 = null;
        string sqlQuery3 = null;
        try
        {
            sqlQuery = "SELECT ObjectiveId, OrderInProject FROM ObjectivesPerProject WHERE ProjectId = " + projectId + " ORDER BY OrderInProject;";
            IDataReader result = db.Read(sqlQuery);
            IDataReader result2 = null;
            IDataReader result3 = null;
            while (result.Read())
            {
                List<Effect> effects = new List<Effect>();
                sqlQuery2 = "SELECT * FROM Objective WHERE ObjectiveId = " + Convert.ToInt32(result["ObjectiveId"]) + ";";
                result2 = db.Read(sqlQuery2);
                sqlQuery3 = "SELECT * FROM Effect WHERE EffectId IN (SELECT EffectId FROM EffectPerObjective WHERE ObjectiveId = " + 
                    Convert.ToInt32(result["ObjectiveId"]) + ");";
                result3 = db.Read(sqlQuery3);
                while(result3.Read())
                {
                    effects.Add(new Effect(Convert.ToInt32(result3["EffectId"]), Convert.ToDouble(result3["Value"]), result3["Indicator"].ToString(),
                        Convert.ToInt32(result3["Duration"])));
                }
                Debug.Log($"{result2["DESCRIPTION"].ToString()}");
                objectives.Enqueue(new Objective(Convert.ToInt32(result2["ObjectiveId"]), result2["DESCRIPTION"].ToString(),
                    Convert.ToInt32(result2["NumberOfSteps"]), Convert.ToInt32(result2["IsCompleted"]) != 0, result2["Type"].ToString(),
                    Convert.ToInt32(result2["TriggersPipeline"]) != 0, Convert.ToInt32(result["OrderInProject"]), effects));
            }

        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery3 + "\n Reason: " + e.Message);
        }

        return objectives;
    }
}