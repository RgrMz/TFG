using Assets.Scripts.Persistance;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BadgeDAO
{
    private Singleton db;
    private string sqlQuery;

    public BadgeDAO()
    {
        db = Singleton.GetInstance();

        sqlQuery = null;
    }

    public int saveBadge(Player player, int badgeId)
    {
        int result = -1;
        try
        {
            sqlQuery = $"INSERT INTO BadgesWon (UserId, BadgeId) VALUES ({player.UserId}, {badgeId});";
            result = db.Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return result;
    }

    public bool hasBadge(Player player, int badgeId)
    {
        bool hasBadge = false;
        try
        {
            sqlQuery = $"SELECT * FROM BadgesWon WHERE UserId = {player.UserId} AND BadgeId = {badgeId};";
            IDataReader result = db.Read(sqlQuery);
            while(result.Read())
            {
                hasBadge = true;
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return hasBadge;
    }

}
