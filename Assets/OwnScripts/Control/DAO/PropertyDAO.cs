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

    public Dictionary<string, double> GetAllProperties(int projectId, string difficulty)
    {
        Dictionary<string, double> properties = new Dictionary<string, double>();
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
                    Debug.Log($"{result2["Name"].ToString()}     {Convert.ToDouble(result2["Value"])}     ");
                    properties.Add(result2["Name"].ToString(), Convert.ToDouble(result2["Value"]));
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
