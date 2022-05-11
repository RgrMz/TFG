using Assets.Scripts.Persistance;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ProblemDAO
{
    private Singleton db;
    private string sqlQuery;
    private EffectDAO effectDAO;
    public ProblemDAO()
    {
        db = Singleton.GetInstance();
        effectDAO = new EffectDAO();
        sqlQuery = null;
    }

    private Solution getSingleSolution(int solutionId)
    {
        Solution solution = null;
        Effect solutionEffect = null;
        List<Effect> solutionEffects = new List<Effect>();
        try
        {
            sqlQuery = "SELECT * FROM Solution WHERE SolutionId = " + solutionId + " ;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                if (solution != null)
                {
                    solutionEffect = effectDAO.getEffect(Convert.ToInt32(result["EffectId"]));
                    solutionEffects.Add(solutionEffect);
                }
                if (solution == null)
                {
                    solution = new Solution(Convert.ToInt32(result["SolutionId"]), (float)result["Cost"], (float)result["Profit"],
                        result["Description"].ToString());
                    solutionEffect = effectDAO.getEffect(Convert.ToInt32(result["EffectId"]));
                    solutionEffects.Add(solutionEffect);
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        solution.Effects = solutionEffects;
        return solution;
    }

    private List<Solution> getSolutionsOfProblem(int problemId)
    {
        int solutionId = 0;
        List<Solution> solutions = new List<Solution>();
        try
        {
            sqlQuery = "SELECT SolutionId FROM Solution WHERE ProblemId = " + problemId + " ;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                if (Convert.ToInt32(result["SolutionId"]) != solutionId)
                {
                    solutionId = Convert.ToInt32(result["SolutionId"]);
                    solutions.Add(getSingleSolution(solutionId));
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }

        return solutions;
    }

    public List<Problem> getAllOProblems()
    {
        List<Problem> problems = new List<Problem>();
        List<Solution> problemSolutions = new List<Solution>();
        try
        {
            sqlQuery = "SELECT * FROM Problem;";
            IDataReader result = db.Read(sqlQuery);
            while (result.Read())
            {
                problemSolutions = getSolutionsOfProblem(Convert.ToInt32(result["ProblemId"]));
                if (problemSolutions.Count != 0)
                {
                    problems.Add(new Problem(Convert.ToInt32(result["ProblemId"]), result["Description"].ToString(), problemSolutions));
                }
            }
        }
        catch (SqliteException e)
        {
            Debug.LogError("[SQL - ERROR] Error while executing the following command: " + sqlQuery + "\n Reason: " + e.Message);
        }
        return problems;
    }
}
