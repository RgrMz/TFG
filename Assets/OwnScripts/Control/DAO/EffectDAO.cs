using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using Assets.Scripts.Persistance;
using System;
using UnityEngine;

public class EffectDAO
{
    private Singleton db;
    private string sqlQuery;

    public EffectDAO()
    {
        db = Singleton.GetInstance();
        sqlQuery = null;
    }

    public Effect getEffect (int effectId)
    {
        Effect resultEffect = null;
        try
        {
            sqlQuery = "SELECT * FROM Effect WHERE EffectId = " + effectId + " ;";
            IDataReader result = db.Read(sqlQuery);
            resultEffect = new Effect(Convert.ToInt32(result["EffectId"]), (float)result["Value"], 
                result["Indicator"].ToString());
        } catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return resultEffect;
    }

}
