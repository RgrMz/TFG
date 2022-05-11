public class Game
{
    public Project PlayedProject {get; set; }

    public Game(Project project)
    {
        PlayedProject = project;
    }

    public Memento saveCurrentObjective()
    {
        return new Memento(PlayedProject.CurrentObjective);
    }

    public void restoreReplacedObjective(Memento m)
    {
        PlayedProject.CurrentObjective = m.ReplacedObjective;
    }

    public class Memento
    {
        public Objective ReplacedObjective { get; set; }

        public Memento(Objective replacedObjective)
        {
            ReplacedObjective = replacedObjective;
        }
    }
}
