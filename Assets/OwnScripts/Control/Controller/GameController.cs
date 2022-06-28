using System.Collections.Generic;
using UnityEngine;
using static Game;

public class GameController
{
    public Game PlayedGame { get; set; }
    public Stack<Memento> History { get; set; }
    private Objective ProblemObjective { get; set; }
    private ObjectiveDAO objectiveDAO;
    private GameDAO gameDao;
    private PlayerDAO playerDao;
    private int numberOfObjectives;

    public GameController()
    {
        objectiveDAO = new ObjectiveDAO();
        gameDao = new GameDAO();
        playerDao = new PlayerDAO();
    }
    
    public GameController(Game game)
    {
        PlayedGame = game;
        History = new Stack<Memento>();
        numberOfObjectives = game.PlayedProject.Objectives.Count;
        objectiveDAO = new ObjectiveDAO();
        gameDao = new GameDAO();

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

    public void updateGameState(string result, string role, string difficulty)
    {        
        PlayedGame.GamePlayer = DataSaver.loadData<Player>("player");
        PlayedGame.Result = result;
        PlayedGame.Role = role;
        PlayedGame.Difficulty = difficulty;
    }

    public int saveGame(Game game)
    {
        return gameDao.saveGame(game);
    }

    public Game getLastGame(Player player)
    {
        return gameDao.getLastGamePlayed(player);
    }
}
