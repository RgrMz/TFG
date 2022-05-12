using UnityEngine;

public class DifficultySelector : MonoBehaviour
{
    public static string Difficulty { get; set; }
    
    public void selectDifficulty()
    {
        Difficulty = gameObject.name.Contains("Basic") ? "Basic" : "Advanced";
        Debug.Log(Difficulty);
    }
}
