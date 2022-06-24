using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Assets.Scripts.Persistance
{
    public class Singleton
    {
        private static Singleton instance = null;
        private readonly string dbConnectionString = "URI=file:" + Application.dataPath + "/StreamingAssets/JTTCDevOps.db";
        private string dbPath = Application.dataPath + "/StreamingAssets/JTTCDevOps.db";
        private static IDbConnection dbConnection;
        private IDbCommand dbCommand;
        private static bool created = false;

        private Singleton()
        {
            dbConnection = new SqliteConnection(dbConnectionString);
            dbConnection.Open();
            Debug.Log(Application.dataPath);

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
}