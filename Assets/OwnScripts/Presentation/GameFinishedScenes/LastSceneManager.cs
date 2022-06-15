using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class LastSceneManager : MonoBehaviour
{
    protected string role;
    protected string difficulty;
    protected int minutes;
    protected int seconds;
    private List<Indicator> indicators;
    private GameController gameController;

    public GameObject indicatorsList;
    public List<TextMeshProUGUI> indicatorValues;
    public TextMeshProUGUI roleText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI calification;
    public TextMeshProUGUI numberOfGamesPlayed;
    public TextMeshProUGUI previousResult;
    public TextMeshProUGUI previousCalification;

    // Start is called before the first frame update
    void Start()
    {
        indicators = new List<Indicator>();
        gameController = new GameController();
        LoadData();
    }

    internal void LoadData()
    {
        roleText.text = PlayerPrefs.GetString("rolePlayed");
        difficultyText.text = PlayerPrefs.GetString("difficulty");
        float secondsPlayed = PlayerPrefs.GetFloat("gameTime");
        string calification = "";

        minutes = (int)secondsPlayed / 60;
        seconds = (int)secondsPlayed % 60;

        timeText.text = $"{minutes} min {seconds} sec";

        float finalScore = 0;

        foreach (Transform child in indicatorsList.transform)
        {
            Indicator indicator = DataSaver.loadData<Indicator>(child.gameObject.name);
            TextMeshProUGUI valueText = indicatorValues.Find(indicator => indicator.name.Contains(child.gameObject.name));
            valueText.text = $"{indicator.Value} %";
            finalScore += CalculateIndicatorScore((int)indicator.Value);
        }

        if (SceneManager.GetActiveScene().name.Equals("WinScene"))
        {
            calification = ShowCalification(finalScore);
        }

        Game gamePlayed = DataSaver.loadData<Game>("game");
        gamePlayed.Calification = calification;

        Game lastGame = gameController.getLastGame(gamePlayed.GamePlayer);
        gameController.saveGame(gamePlayed);
        
        if (lastGame != null)
        {
            previousResult.text = lastGame.Result;
            ++lastGame.GameNumber;
            numberOfGamesPlayed.text = lastGame.GameNumber.ToString();
            if (!lastGame.Calification.Equals(""))
            {
                previousCalification.text = lastGame.Calification;
            }
        }
        else
        {
            previousResult.text = "No previous game";
            numberOfGamesPlayed.text = "1";
            previousCalification.text = "No";
        }
    }

    private string ShowCalification(float finalScore)
    {
        int index = -1;
        List<float> scores = new List<float>() { 0, 1, 2, 3, 4, 5 };
        List<string> califications = new List<string>() { "D", "C", "B", "A", "S" };
        for (int i = 0; i < scores.Count - 1;)
        {
            if (finalScore >= scores[i] && finalScore < scores[++i])
            {
                index = i - 1;
                break;
            }
        }

        if (index != -1)
        {
            calification.text = califications[index];
            return califications[index];
        }
        else
        {
            return null;
        }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    private float CalculateIndicatorScore(int indicatorValue)
    {
        int index = 0;
        List<int> valueMilestones = new List<int>() { 0, 16, 32, 48, 64, 80, 100 };
        List<float> scores = new List<float>() { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1 };
        for (int i = 0; i < valueMilestones.Count - 1;)
        {
            if (indicatorValue >= valueMilestones[i] && indicatorValue < valueMilestones[++i])
            {
                index = i - 1;
                break;
            }
        }

        return scores[index];
    }
}
