using UnityEngine;

public class TaskSetter : MonoBehaviour
{
    public string TaskName;
    public string TaskDescription;
    TaskUI taskUI;
    void Start()
    {
        taskUI = FindAnyObjectByType<TaskUI>();
        taskUI.Setup(TaskName, TaskDescription);
    }

    private void OnDisable()
    {
        if (taskUI)
        {
            taskUI.Disable();
            taskUI = null;
        }
    }
}
