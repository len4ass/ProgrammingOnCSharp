using System.Collections.Generic;

internal sealed class Support
{
    private List<Task> _tasks = new List<Task>();

    public IEnumerable<Task> Tasks => _tasks;

    public int OpenTask(string text)
    {
        int taskId = _tasks.Count + 1;
        var newTask = new Task(taskId, text);
        _tasks.Add(newTask);
        return taskId;
    }

    public void CloseTask(int id, string answer)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == id)
            {
                task.IsResolved = true;
                task.Answer = answer;
            }
        }
    }

    public List<Task> GetAllUnresolvedTasks()
    {
        var unresolved = new List<Task>();

        foreach (var task in _tasks)
        {
            if (!task.IsResolved)
            {
                unresolved.Add(task);
            }
        }

        return unresolved;
    }

    public void CloseAllUnresolvedTasksWithDefaultAnswer(string answer)
    {
        foreach (var task in _tasks)
        {
            if (!task.IsResolved)
            {
                task.IsResolved = true;
                task.Answer = answer;
            }
        }
    }

    public string GetTaskInfo(int id)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == id)
            {
                return task.ToString();
            }
        }

        return null;
    }
        
}