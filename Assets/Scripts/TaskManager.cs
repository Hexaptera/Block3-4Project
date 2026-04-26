using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] string[] tasks;
    [SerializeField] GameObject taskDisplayPrefab;
    public TaskDisplay[] taskDisplays;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskDisplays = new TaskDisplay[tasks.Length];
        for (int i = 0; i < tasks.Length; i++) 
        {
            GameObject newTask = Instantiate(taskDisplayPrefab, canvas.transform);
            newTask.GetComponent<TextMeshProUGUI>().text = tasks[i];
            Debug.Log(newTask.GetComponent<TaskDisplay>());
            taskDisplays[i] = newTask.GetComponent<TaskDisplay>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
