using Assets.Scripts.Persistance;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class PlayerDAO
{
    private Singleton db;
    private string sqlQuery;
    private EffectDAO effectDAO;
    public PlayerDAO()
    {
        db = Singleton.GetInstance();
        effectDAO = new EffectDAO();
        sqlQuery = null;
    }

    private List<Badge> getAllBadgesFrom(int userId)
    {
        List<Badge> badges = new List<Badge>();
        try
        {
            sqlQuery = $"SELECT * from BADGE WHERE BadgeId IN (SELECT BadgeId FROM BadgesWon WHERE UserId = {userId});";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                Badge badge = new Badge(Convert.ToInt32(result["BadgeId"]), result["Description"].ToString(),
                    result["ImagePath"].ToString(), result["Title"].ToString());
                badges.Add(badge);
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return badges;
    }

    public List<Player> getAllPlayers()
    {
        List<Player> players = new List<Player>();
        try
        {
            sqlQuery = "SELECT * FROM Player;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                Player player = new Player(Convert.ToInt32(result["UserId"]), result["UserName"].ToString(), Convert.ToInt32(result["Age"]));
                player.WonBadges = getAllBadgesFrom(player.UserId);
                players.Add(player);
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return players;
    }

    public int savePlayer(Player player)
    {
        int result = -1;

        try
        {
            string sqlQuery = $"INSERT INTO Player(Age, UserName) VALUES({player.Age}, {player.UserName});";

            result = db.Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.Log("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        return result;
    }

}
