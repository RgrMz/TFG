using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using Assets.Scripts.Persistance;
using System;
using UnityEngine;

public class ProjectDAO
{
    private Singleton db;
    private string sqlQuery;

    public ProjectDAO()
    {
        db = Singleton.GetInstance();
        sqlQuery = null;
    }

    public List<Project> GetAllProjects()
    {
        List<Project> projects = new List<Project>();
        try
        {
            sqlQuery = "SELECT * FROM PROJECT;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                projects.Add(new Project(Convert.ToInt32(result["ProjectId"]), result["DESCRIPTION"].ToString(), result["TYPE"].ToString()));
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        return projects;
    }
}