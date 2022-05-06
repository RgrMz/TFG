using UnityEngine;
using TMPro;

// Class in charge of loading the project and objectives based on player's decission
public class GameManager : MonoBehaviour, IObjectiveSwitchHandler
{
    public TextMeshProUGUI objectiveText;

    public TextMeshProUGUI problemsAndProjectText;

    public TextMeshProUGUI completedObjectiveText;

    public GameObject completedObjectiveTextBackground;

    protected GameObject mostRecentArrow;

    protected ProjectController projectController;
    protected static string role;
    // protected GameController gameController; => Save the current generated game in DB
    // This is for quick simulations of an started game:
    protected Project projectForQuickSimulation = new Project(1, "This project consists of operating the new code developed by the development team based on the legacy one of the Information System of the restaurant GoodSushi.They know its Information System has quality flaws along with performance issues and mid - to - long downtimes.The software will be deployed to Amazon Web Service servers in the cloud, so it will be monitored and operated thorugh services of this platform such as Amazon CloudFormation or Amazon CloudWatch", "Dev");
    private string Difficulty { get; set; }
    private void Start()
    {
        Difficulty = DifficultySelector.Difficulty;
        projectController = new ProjectController();
        Cursor.visible = false;
    }
    private void Update()
    {
        if (projectController.SelectedProject != null)
        {
            Debug.Log(projectController.SelectedProject.CurrentObjective.IsCompleted);
            if (projectController.SelectedProject.CurrentObjective.IsCompleted)
            {
                Destroy(mostRecentArrow);
                projectController.SelectedProject.CurrentObjective = projectController.SelectedProject.Objectives.Dequeue();
                objectiveText.text = projectController.SelectedProject.CurrentObjective.Description;
                if (objectiveText.text.Contains("operations team") || objectiveText.text.Contains("development team"))
                {
                    // use the role parameter to know which floor to go
                }
                else
                {
                    mostRecentArrow = SpawnArrowForPlaceIndication(projectController.SelectedProject.CurrentObjective.Place);
                }
            }
        }
    }
    private void InitializeGame()
    {
        // if statement used for quick simulation 
        //if (projectForQuickSimulation != null)
        //{
        //    problemsAndProjectText.text = projectForQuickSimulation.Description;
        //}
        //else
        //{
        //    projectController.SelectedProject = projectController.SelectRandomProject(role, Difficulty);
        //    problemsAndProjectText.text = projectController.SelectedProject.Description;
        //}
        projectController.SelectedProject = projectController.SelectRandomProject(role, Difficulty);
        problemsAndProjectText.text = projectController.SelectedProject.Description;
    }

    public void ObjectiveProgressed()
    {
        if (IsObjectiveCompleted())
        {
            projectController.SelectedProject.CurrentObjective.IsCompleted = true;
            StartCoroutine(TextFadingInAndOut.FadeText(completedObjectiveTextBackground, 2f, completedObjectiveText));
        }
        else
        {
            projectController.SelectedProject.CurrentObjective.CurrentStep++;
        }
    }

    public void RoleSelected(string playerRole)
    {
        role = playerRole;
        InitializeGame();
    }

    private GameObject SpawnArrowForPlaceIndication(string place, string role = null)
    {
        Debug.Log(place);
        GameObject ceilToSpawnArrow = GameObject.Find($"{place}ArrowReference");
        Transform ceilTransform = ceilToSpawnArrow.transform;
        Vector3 positionToSpawn = new Vector3(ceilTransform.position.x, ceilTransform.position.y + 12, ceilTransform.position.z);
        GameObject arrow = Instantiate(Resources.Load("OwnPrefabs/ArrowPointObjectivePlace"), positionToSpawn, Quaternion.Euler(90, 90, 0)) as GameObject;
        return arrow;
    }

    private bool IsObjectiveCompleted()
    {
        return (projectController.SelectedProject.CurrentObjective.NumberOfSteps > 1 &&
            projectController.SelectedProject.CurrentObjective.CurrentStep < projectController.SelectedProject.CurrentObjective.NumberOfSteps);
        
    }

}
