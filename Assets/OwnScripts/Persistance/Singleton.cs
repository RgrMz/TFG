using System.Collections.Generic;
using Mono.Data.Sqlite;
using System.IO;
using UnityEngine;
using System.Data;

public class Singleton
{
    private static Singleton instance = null;
    private readonly string dbConnectionString = "URI=file:" + Application.dataPath + "/Database/" + "JTTCDevOps.db";
    private string dbPath = Application.dataPath + "/Database/" + "Global-ManagerDB.db";
    private static IDbConnection dbConnection;
    private IDbCommand dbCommand;
    private static bool created = false;

    private Singleton()
    {
        dbConnection = new SqliteConnection(dbConnectionString);
        dbConnection.Open();

        Debug.Log($"[Singleton - INFO] Database connection into the path: {dbPath}");

    }

    public static Singleton GetInstance()
    {
        if (instance == null)
        {
            instance = new Singleton();
        }

        return instance;
    }

    /*private void GenerateDatabase()
    {
        if (!created)
        {
            string sqlQuery = File.ReadAllText(@".\Assets\Scripts\Persistence\CreateDB.sql");
            //Debug.Log($"[Singleton - INFO] SQL QUERY = {sqlQuery}");

            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteNonQuery();

            sqlQuery = File.ReadAllText(@".\Assets\Scripts\Persistence\Prueba.sql");
            //Debug.Log($"[Singleton - INFO] SQL QUERY = {sqlQuery}");

            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sqlQuery;
            dbCommand.ExecuteNonQuery();

            created = true;

            Debug.Log($"[Singleton - INFO] Database created into the path: {dbPath}");
        }
    }*/

    public IDataReader Read(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        //Debug.Log($"[Singleton - INFO] SQL QUERY = {sqlQuery}");

        IDataReader result = dbCommand.ExecuteReader();

        return result;
    }

    public int Insert(string sqlQuery)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = sqlQuery;
        //Debug.Log($"[Singleton - INFO] SQL QUERY = {sqlQuery}");

        int result = dbCommand.ExecuteNonQuery();

        //CloseDB();

        return result;
    }

    public static void CloseDB()
    {
        dbConnection.Close();
    }

}
