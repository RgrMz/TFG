using UnityEngine;
using TMPro;

// Class in charge of loading the project and objectives based on player's decission
public class GameInitializer : MonoBehaviour, IObjectiveSwitchHandler
{
    public TextMeshProUGUI objectiveText;

    protected ProjectController projectController;
    protected static string role;
    // protected GameController gameController; => Save the current generated game in DB
    private string Difficulty { get; set; }
    private void Start()
    {
        Difficulty = DifficultySelector.Difficulty;
        projectController = new ProjectController();
        Cursor.visible = false;
    }
    private void Update()
    {
        if(projectController.SelectedProject != null)
        {
            if (projectController.SelectedProject.CurrentObjective.IsCompleted)
            {
                projectController.SelectedProject.CurrentObjective = projectController.SelectedProject.Objectives.Dequeue();
                objectiveText.text = projectController.SelectedProject.CurrentObjective.Description;
            }
        }
    }
    private void InitializeGame()
    {
        projectController.SelectedProject = projectController.SelectRandomProject(role, Difficulty);
    }

    public void ObjectiveCompleted()
    {
        projectController.SelectedProject.CurrentObjective.IsCompleted = true;
        Debug.Log("Message received");
    }
    public void RoleSelected(string playerRole)
    {
        role = playerRole;
        InitializeGame();
    }
}
