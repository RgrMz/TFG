using Mono.Data.Sqlite;
using System;
using Assets.Scripts.Persistance;
using System.Data;
using UnityEngine;

public class GameDAO
{

    private Singleton db;
    private string sqlQuery;
    private PlayerDAO playerDao;
    public GameDAO()
    {
        db = Singleton.GetInstance();
        playerDao = new PlayerDAO();
        sqlQuery = null;
    }


    public int saveGame(Game playedGame)
    {
        int result = -1;
        int userId = playerDao.getUserId(playedGame.GamePlayer.UserName, playedGame.GamePlayer.Age);
        int gameNumber = 0;

        // Get the number of games a given player has played
        try
        {
            string sqlQuery2 = $"SELECT GameNumber FROM PlayedGame WHERE UserId = {userId} ORDER BY GameNumber DESC LIMIT 1;";
            IDataReader result2 = db.Read(sqlQuery2);
            while(result2.Read())
            {
                gameNumber = Convert.ToInt32(result2["GameNumber"]);
            }
            if (gameNumber >= 0)
            {
                gameNumber++;
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        try
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            sqlQuery = $"INSERT INTO PlayedGame (GameId, UserId, Result, Calification, Role, Difficulty, GameNumber) " +
                $"VALUES ('{myuuidAsString}', {userId}, '{playedGame.Result}', '{playedGame.Calification}', " +
                $"'{playedGame.Role}', '{playedGame.Difficulty}', {gameNumber});";

            result = db.Insert(sqlQuery);
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return result;
    }

    public Game getLastGamePlayed(Player player)
    {
        Game lastGame = null;

        try
        {
            sqlQuery = $"SELECT * FROM PlayedGame WHERE UserId = {player.UserId} ORDER BY GameNumber DESC LIMIT 1;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                lastGame = new Game();
                lastGame.Calification = result["Calification"].ToString();
                lastGame.GameNumber = Convert.ToInt32(result["GameNumber"]);
                lastGame.Result = result["Result"].ToString();
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        return lastGame;
    }
}
