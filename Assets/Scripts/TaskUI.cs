using UnityEngine;
using TMPro;
public class TaskUI : MonoBehaviour
{
    public GameObject TaskParent;
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI taskDescriptionText;
    public GameObject taskCompletePrefab;

    

    public void Setup(string task,string des)
    {
        taskText.text = task;
        taskDescriptionText.text = des;
        TaskParent.SetActive(true);
    }

    public void Disable()
    {
        TaskParent.SetActive(false);
    }


    public void TaskComplete()
    {
        Instantiate(taskCompletePrefab, transform);
    }
}
