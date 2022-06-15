using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectController
{
    private ProjectDAO projectDao;
    private ObjectiveDAO objectiveDAO;
    private PropertyDAO propertyDAO;
    private ProblemDAO problemDAO;
    private List<Project> Projects { get; set; }
    public Project SelectedProject { get; set; }

    private static readonly System.Random random = new System.Random();
    private const int NUMBER_OF_EVENTS = 4;
    public List<int> objectivesToGenerateProblems;
    private int problemsCouldAppearAfter;
    private int problemsCouldAppearBefore;
    private const int OBJECTIVE_WATCH_PIPELINE = 13;
    private const int PUSH_CHANGES_DEV = 12;
    private const int PUSH_CHANGES_OPS = 26;
    
    public ProjectController()
    {
        projectDao = new ProjectDAO();
        objectiveDAO = new ObjectiveDAO();
        propertyDAO = new PropertyDAO();
        problemDAO = new ProblemDAO();
        Projects = LoadProjects();
        // After the first objective, the problems might start to appear
        problemsCouldAppearAfter = 1;
        problemsCouldAppearBefore = 2;
    }

    private List<Project> LoadProjects()
    {
        return projectDao.GetAllProjects();
    }

    public Project SelectRandomProject(string role, string difficulty)
    {
        List<Project> possibleProjects = new List<Project>();
        Project selectedProject = new Project();
        foreach (Project p in Projects)
        {
            if (p.Type.Equals(role))
            {
                possibleProjects.Add(p);
            }
        }

        int randomIndex = random.Next(possibleProjects.Count);
        selectedProject = possibleProjects[randomIndex];

        selectedProject.Objectives = objectiveDAO.GetAllObjectives(selectedProject.Id);
        selectedProject.Properties = propertyDAO.GetAllProperties(selectedProject.Id, difficulty);

        List<Problem> allProblems = problemDAO.getAllOProblems();

        initializeProblems(selectedProject, allProblems);

        return selectedProject;
    }

    private void initializeProblems(Project selectedProject, List<Problem> allProblems)
    {
        Problem randomProblem = null;
        while (selectedProject.Problems.Count < NUMBER_OF_EVENTS)
        {
            do
            {
                randomProblem = allProblems[random.Next(allProblems.Count)];
            } while (selectedProject.Problems.Exists(problem => randomProblem.ProblemId == problem.ProblemId));
            selectedProject.Problems.Add(randomProblem);
        }

        // Randomly generates the objectiveIDs after which completion a problem will be generated
        objectivesToGenerateProblems = new List<int>();
        int lastSelected = -10; // Make it sure the first random objective number is picked

        // Get all objectives Id of the selected project
        List<int> projectObjectivesId = new List<int>();
        //foreach (Objective o in selectedProject.Objectives)
        //{
        //    projectObjectivesId.Add(o.ObjectiveId);
        //}

        //while (objectivesToGenerateProblems.Count < NUMBER_OF_EVENTS)
        //{
        //    int index = random.Next(problemsCouldAppearAfter, projectObjectivesId.Count - problemsCouldAppearBefore);
        //    int randomObjectiveId = projectObjectivesId[index];
        //    bool isAForbiddenObjectiveId = (randomObjectiveId == OBJECTIVE_WATCH_PIPELINE || randomObjectiveId == PUSH_CHANGES_DEV
        //        || randomObjectiveId == PUSH_CHANGES_OPS);
        //    if (objectivesToGenerateProblems.Contains(randomObjectiveId) || Math.Abs(randomObjectiveId - lastSelected) < 2 || isAForbiddenObjectiveId)
        //    {
        //        continue;
        //    }

        //    lastSelected = randomObjectiveId;
        //    Debug.Log(randomObjectiveId);
        //    objectivesToGenerateProblems.Add(randomObjectiveId);
        //}

        // For quick simulation
        /* controlar que ninguno de esos IDs sea el de watch the pipeline execution ! */
        objectivesToGenerateProblems = new List<int>() { 7, 20, 9, 10 };
    }
}
