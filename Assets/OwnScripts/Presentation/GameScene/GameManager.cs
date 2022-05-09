using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// Class in charge of loading the project and objectives based on player's decission
public class GameManager : MonoBehaviour, IObjectiveSwitchHandler
{
    public GameObject indicatorsManagerGO;

    public TextMeshProUGUI objectiveText;

    public TextMeshProUGUI problemsAndProjectText;

    public TextMeshProUGUI completedObjectiveText;

    public GameObject completedObjectiveTextBackground;

    protected GameObject mostRecentArrow;

    protected GameObject player;

    protected GameObject[] objectiveHandlerList;

    public GameObject pipelineManager;

    public ProjectController projectController;
    public int objectivesCompleted;
    public bool projectSelected;
    protected static string role;
    // protected GameController gameController; => Save the current generated game in DB
    // This is for quick simulations of an started game:
    protected Project projectForQuickSimulation = new Project(1, "This project consists of operating the new code developed by the development team based on the legacy one of the Information System of the restaurant GoodSushi.They know its Information System has quality flaws along with performance issues and mid - to - long downtimes.The software will be deployed to Amazon Web Service servers in the cloud, so it will be monitored and operated thorugh services of this platform such as Amazon CloudFormation or Amazon CloudWatch", "Dev");
    public string Difficulty { get; set; }
    private void Awake()
    {
        indicatorsManagerGO = GameObject.Find("IndicatorsManager");
        // For quick simulations
        if (DifficultySelector.Difficulty == null)
        {
            Difficulty = "Basic";
        }
        else
        {
            Difficulty = DifficultySelector.Difficulty;
        }
        projectController = new ProjectController();
        Cursor.visible = false;
        objectivesCompleted = 0;
        projectSelected = false;
        player = GameObject.FindWithTag("Player");
        objectiveHandlerList = GameObject.FindGameObjectsWithTag("ObjectiveHandler");
    }
    private void Update()
    {
        if (projectController.SelectedProject != null)
        {
            if (projectController.SelectedProject.CurrentObjective.IsCompleted)
            {
                if(projectController.SelectedProject.CurrentObjective.TriggersPipeline)
                {
                    pipelineManager.GetComponent<PipelineExecution>().StartExecution();
                }

                if(projectController.SelectedProject.CurrentObjective.Effects != null)
                {
                    foreach(Effect effect in projectController.SelectedProject.CurrentObjective.Effects)
                    {
                        indicatorsManagerGO.GetComponent<IndicatorsManager>().ApplyEffect(effect);
                    }
                }

                objectivesCompleted++;
                indicatorsManagerGO.GetComponent<IndicatorsManager>().UpdateFunctionalityBar();

                Destroy(mostRecentArrow);

                projectController.SelectedProject.CurrentObjective = projectController.SelectedProject.Objectives.Dequeue();
                objectiveText.text = projectController.SelectedProject.CurrentObjective.Description;
                Debug.Log($"The current objective triggers pipeline at start? : {projectController.SelectedProject.CurrentObjective.TriggersPipeline}");
                if (projectController.SelectedProject.CurrentObjective.TriggersPipeline)
                {
                    pipelineManager.GetComponent<PipelineExecution>().StartExecution();
                }

                mostRecentArrow = SpawnArrowForPlaceIndication(projectController.SelectedProject.CurrentObjective.Place);

                foreach(GameObject objectiveHandlerGO in objectiveHandlerList)
                {
                    ExecuteEvents.Execute<ObjectiveHandler>(
                        objectiveHandlerGO, null, (handler, y) => handler.UpdateCurrentObjectivePlace(projectController.SelectedProject.CurrentObjective.Place));
                }
                ExecuteEvents.Execute<ObjectiveHandlerAnimations>(
                    player, null, (handler, y) => handler.UpdateCurrentObjectivePlace(projectController.SelectedProject.CurrentObjective.Place));
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

        // Initialize the indicators
        indicatorsManagerGO.GetComponent<IndicatorsManager>().InitializeIndicators();

        // Tell the indicators manager to start applying the progress to the bars
        indicatorsManagerGO.GetComponent<IndicatorsManager>().StartMakingBarsProgress();
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

    private GameObject SpawnArrowForPlaceIndication(string place)
    {
        GameObject ceilToSpawnArrow = GameObject.Find($"{place}ArrowReference");
        Transform ceilTransform = ceilToSpawnArrow.transform;
        Vector3 positionToSpawn = new Vector3(ceilTransform.position.x, ceilTransform.position.y + 12, ceilTransform.position.z);
        GameObject arrow = Instantiate(Resources.Load("OwnPrefabs/ArrowPointObjectivePlace"), positionToSpawn, Quaternion.Euler(90, 90, 0)) as GameObject;
        return arrow;
    }

    private bool IsObjectiveCompleted()
    {
        return !(projectController.SelectedProject.CurrentObjective.NumberOfSteps > 1 &&
            projectController.SelectedProject.CurrentObjective.CurrentStep < projectController.SelectedProject.CurrentObjective.NumberOfSteps);
    }

}