using System;
using UnityEngine;

[Serializable]
public class Game
{
    public Project PlayedProject {get; set; }

    [SerializeField]
    public Player _player;
    public Player GamePlayer { get { return _player; } set { _player = value; } }
    [SerializeField]
    public string _result;
    public string Result { get { return _result; } set { _result = value; } }
    [SerializeField]
    public string _calification;
    public string Calification { get { return _calification; } set { _calification = value; } }
    [SerializeField]
    public string _role;
    public string Role { get { return _role; } set { _role = value; } }
    [SerializeField]
    public string _difficulty;
    public string Difficulty { get { return _difficulty; } set { _difficulty = value; } }
    [SerializeField]
    public int _gameNumber;
    public int GameNumber { get { return _gameNumber; } set { _gameNumber = value; } }

    public Game()
    {

    }

    public Game(Project project)
    {
        PlayedProject = project;
    }

    public Memento saveCurrentObjective()
    {
        return new Memento(PlayedProject.CurrentObjective);
    }

    public void restoreReplacedObjective(Memento m)
    {
        PlayedProject.CurrentObjective = m.ReplacedObjective;
    }

    public class Memento
    {
        public Objective ReplacedObjective { get; set; }

        public Memento(Objective replacedObjective)
        {
            ReplacedObjective = replacedObjective;
        }
    }
}
