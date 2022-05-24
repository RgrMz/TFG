using UnityEngine;
using UnityEngine.UI;

public class DifficultySelector : MonoBehaviour
{
    [SerializeField] private Button startGame;
    public static string Difficulty { get; set; }

    private void Update()
    {
        if (Difficulty == null)
        {
            startGame.interactable = false;
        }
        else
        {
            startGame.interactable = true;
        }
    }

    public void selectDifficulty()
    {
        Difficulty = gameObject.name.Contains("Basic") ? "Basic" : "Advanced";
        Debug.Log(Difficulty);
    }
}
