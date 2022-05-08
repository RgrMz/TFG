using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using Assets.Scripts.Persistance;
using System;
using UnityEngine;

public class PropertyDAO
{
    private Singleton db;
    private string sqlQuery;

    public PropertyDAO()
    {
        db = Singleton.GetInstance();
        sqlQuery = null;
    }

    public Dictionary<string, float> GetAllProperties(int projectId, string difficulty)
    {
        Dictionary<string, float> properties = new Dictionary<string, float>();
        string sqlQuery2;
        try
        {
            sqlQuery = "SELECT PropertyId FROM PropertyPerProject WHERE ProjectId = " + projectId + ";";
            IDataReader result = db.Read(sqlQuery);
            IDataReader result2 = null;
            while (result.Read())
            {
                sqlQuery2 = "SELECT * FROM Property WHERE PropertyId = " + Convert.ToInt32(result["PropertyId"]) + " AND Difficulty = '" + difficulty + "' ;";
                result2 = db.Read(sqlQuery2);
                if (result2.Read())
                {
                    properties.Add(result2["Name"].ToString(), (float) (result2["Value"]));
                }
            }

        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        return properties;
    }
}
