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

    public GameObject indicatorsList;
    public List<TextMeshProUGUI> indicatorValues;
    public TextMeshProUGUI roleText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI calification;

    // Start is called before the first frame update
    void Start()
    {
        indicators = new List<Indicator>();
        LoadData();
    }

    internal void LoadData()
    {
        roleText.text = PlayerPrefs.GetString("rolePlayed");
        difficultyText.text = PlayerPrefs.GetString("difficulty");
        float secondsPlayed = PlayerPrefs.GetFloat("gameTime");

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
            string calification = ShowCalification(finalScore);
        }

    }

    private string ShowCalification(float finalScore)
    {
        int index = 0;
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

        if (calification != null)
        {
            calification.text = califications[index];
        }

        return califications[index]; 
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
