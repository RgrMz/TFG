using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System;

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

    public List<GameObject> devopsWall;

    public TextMeshProUGUI[] solutionBoardsText;

    public GameObject pipelineManager;

    public GameObject projectAndProblemShowingBoard;

    public ProjectController projectController;
    public GameController gameController;
    public Problem CurrentProblem { get; set; }
    public int objectivesCompleted;
    public bool projectSelected;
    private bool problemGenerated;
    protected static string role;
    private const int GENERIC_PROBLEM_OBJECTIVE_ID = 4;
    private const int PUSH_TO_REPO_OBJECTIVE_ID = 13;
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
        projectSelected = problemGenerated = false;
        player = GameObject.FindWithTag("Player");
        objectiveHandlerList = GameObject.FindGameObjectsWithTag("ObjectiveHandler");
    }

    private void Update()
    {
        if (projectController.SelectedProject != null)
        {

            if (projectController.objectivesToGenerateProblems.Contains
                (projectController.SelectedProject.CurrentObjective.ObjectiveId) && !problemGenerated)
            {
                // Problem is used for displaying its data in the game's world
                int i = projectController.SelectedProject.CurrentObjective.ObjectiveId;
                CurrentProblem = gameController.selectProblem();
                ApplyProblem();
                gameController.saveCurrentObjective();
                ManageObjectiveChange();
                problemGenerated = true;
                projectController.objectivesToGenerateProblems.Remove(i);
            }

            if (projectController.SelectedProject.CurrentObjective.IsCompleted)
            {

                if (projectController.SelectedProject.CurrentObjective.TriggersPipeline)
                {
                    pipelineManager.GetComponent<PipelineExecution>().StartExecution();
                }

                if (projectController.SelectedProject.CurrentObjective.Effects != null) // modificar condicion => check empty
                {
                    foreach (Effect effect in projectController.SelectedProject.CurrentObjective.Effects)
                    {
                        indicatorsManagerGO.GetComponent<IndicatorsManager>().ApplyEffect(effect);
                    }
                }

                objectivesCompleted++;
                if (objectivesCompleted % 4 == 0)
                {

                }
                if (projectController.SelectedProject.CurrentObjective.ObjectiveId != GENERIC_PROBLEM_OBJECTIVE_ID)
                {
                    indicatorsManagerGO.GetComponent<IndicatorsManager>().UpdateFunctionalityBar();

                    projectController.SelectedProject.CurrentObjective = projectController.SelectedProject.Objectives.Dequeue();

                    ManageObjectiveChange();

                    if (projectController.SelectedProject.CurrentObjective.TriggersPipeline)
                    {
                        pipelineManager.GetComponent<PipelineExecution>().StartExecution();
                    }

                    if (projectController.SelectedProject.CurrentObjective.ObjectiveId == PUSH_TO_REPO_OBJECTIVE_ID)
                    {
                        pipelineManager.GetComponent<PipelineExecution>().NotifyBallsNeeded(projectController.SelectedProject.CurrentObjective.NumberOfSteps);
                    }
                }
                else
                {
                    // Restore the current objective, also today check how to apply effects correctly so that original values are restored
                    gameController.PlayedGame.restoreReplacedObjective(gameController.History.Pop());
                    ManageObjectiveChange();
                    problemGenerated = false;
                }
            }

        }
    }

    private void ApplyProblem()
    {
        problemsAndProjectText.text = CurrentProblem.Description;
        for (int i = 0; i < CurrentProblem.Solutions.Count; i++)
        {
            solutionBoardsText[i].text = CurrentProblem.Solutions[i].Description;
        }

        // Avoid the completion of the problem objective by interacting with the main board
        projectAndProblemShowingBoard.GetComponent<BoxCollider>().enabled = false;
    }

    private void ManageObjectiveChange()
    {
        Destroy(mostRecentArrow);
        if (projectController.SelectedProject.CurrentObjective.ObjectiveId == GENERIC_PROBLEM_OBJECTIVE_ID)
        {
            gameObject.GetComponent<ProblemNotificationHandler>().SpawnOrDespawn(projectController.SelectedProject.CurrentObjective.Description, 1);
            objectiveText.text = "";
        }
        else
        {
            objectiveText.text = projectController.SelectedProject.CurrentObjective.Description;
        }

        mostRecentArrow = SpawnArrowForPlaceIndication(projectController.SelectedProject.CurrentObjective.Place);

        foreach (GameObject objectiveHandlerGO in objectiveHandlerList)
        {
            ExecuteEvents.Execute<ObjectiveHandler>(
                objectiveHandlerGO, null, (handler, y) => handler.UpdateCurrentObjectivePlace(projectController.SelectedProject.CurrentObjective.Place));
        }
        ExecuteEvents.Execute<ObjectiveHandlerAnimations>(
            player, null, (handler, y) => handler.UpdateCurrentObjectivePlace(projectController.SelectedProject.CurrentObjective.Place));
    }

    private void InitializeGame()
    {
        projectController.SelectedProject = projectController.SelectRandomProject(role, Difficulty);
        problemsAndProjectText.text = projectController.SelectedProject.Description;
        gameController = new GameController(new Game(projectController.SelectedProject));

        // Initialize the indicators
        indicatorsManagerGO.GetComponent<IndicatorsManager>().InitializeIndicators();

        // Tell the indicators manager to start applying the progress to the bars
        indicatorsManagerGO.GetComponent<IndicatorsManager>().
            StartMakingBarsProgress(projectController.SelectedProject.Properties["InitialBudget"], projectController.SelectedProject.Properties["InitialDuration"]);
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

    public void SolutionChoosed(string boardName)
    {
        int solutionNumber = boardName[boardName.Length - 1] - '0';
        Debug.Log(solutionNumber);
        // Apply the effects of the solution to the generic current objective for problems
        Solution solution = CurrentProblem.Solutions[solutionNumber - 1];
        if (solution.Cost != 0 || solution.Profit != 0)
        {
            indicatorsManagerGO.GetComponent<IndicatorsManager>().ApplyCostAndProfit(solution.Cost, solution.Profit);
        }

        gameObject.GetComponent<ProblemNotificationHandler>().SpawnOrDespawn("", 0);

        projectController.SelectedProject.CurrentObjective.Effects = solution.Effects;
        ObjectiveProgressed();
    }

    private void DeletePieceOfWall()
    {
        GameObject pieceOfWall = devopsWall[0];
        Destroy(pieceOfWall);
        devopsWall.RemoveAt(0);
    }
}