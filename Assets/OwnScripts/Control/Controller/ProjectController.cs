using System;
using System.Collections.Generic;
using UnityEngine;

public class ProjectController
{
    private ProjectDAO projectDao;
    private ObjectiveDAO objectiveDAO;
    private PropertyDAO propertyDAO;
    private List<Project> Projects { get; set; }
    public Project SelectedProject { get; set; }

    public ProjectController()
    {
        projectDao = new ProjectDAO();
        objectiveDAO = new ObjectiveDAO();
        propertyDAO = new PropertyDAO();
        Projects = LoadProjects();
    }

    private List<Project> LoadProjects()
    {
        return projectDao.GetAllProjects();
    }

    public Project SelectRandomProject(string role, string difficulty)
    {
        List<Project> possibleProjects = new List<Project>();
        Project selectedProject = new Project();
        foreach(Project p in Projects)
        {
            if (p.Type.Equals(role))
            {
                possibleProjects.Add(p);
            }
        }

        int randomIndex = new System.Random().Next(possibleProjects.Count);
        selectedProject = possibleProjects[randomIndex];
        selectedProject.Objectives = objectiveDAO.GetAllObjectives(selectedProject.Id);
        selectedProject.Properties = propertyDAO.GetAllProperties(selectedProject.Id, difficulty);

        return selectedProject;
    }
}
