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

    public ProjectController()
    {
        projectDao = new ProjectDAO();
        objectiveDAO = new ObjectiveDAO();
        propertyDAO = new PropertyDAO();
        problemDAO = new ProblemDAO();
        Projects = LoadProjects();
        // After the first 3 objectives, the problems might start to appear
        problemsCouldAppearAfter = 4;
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

        Problem randomProblem = null;
        List<Problem> allProblems = problemDAO.getAllOProblems();

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
        /*while (objectivesToGenerateProblems.Count < NUMBER_OF_EVENTS)
        {
            int randomObjectiveNumber = random.Next(selectedProject.Objectives.Count - problemsCouldAppearAfter, selectedProject.Objectives.Count - problemsCouldAppearBefore);
            if (objectivesToGenerateProblems.Contains(randomObjectiveNumber) || Math.Abs(randomObjectiveNumber - lastSelected) < 2)
            {
                continue;
            }
            
            lastSelected = randomObjectiveNumber;
            objectivesToGenerateProblems.Add(randomObjectiveNumber);
        }*/

        // For quick simulation
        objectivesToGenerateProblems = new List<int>() { 4, 6, 9, 10 };
        return selectedProject;
    }

}
