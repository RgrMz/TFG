
using System;
using System.Collections.Generic;
using static Game;
using UnityEngine;

public class GameController
{
    public Game PlayedGame { get; set; }
    public Stack<Memento> History { get; set; }
    private Objective ProblemObjective { get; set; }
    private ObjectiveDAO objectiveDAO;
    private int numberOfObjectives;
    
    public GameController(Game game)
    {
        PlayedGame = game;
        History = new Stack<Memento>();
        numberOfObjectives = game.PlayedProject.Objectives.Count;
        objectiveDAO = new ObjectiveDAO();
    }

    public void saveCurrentObjective()
    {
        Memento m = PlayedGame.saveCurrentObjective();
        History.Push(m);
        PlayedGame.PlayedProject.CurrentObjective = objectiveDAO.getGenericProblemObjective();
    }

    public Problem selectProblem()
    {
        Problem problem = PlayedGame.PlayedProject.Problems[0];
        PlayedGame.PlayedProject.Problems.RemoveAt(0);
        return problem;
    }
}
