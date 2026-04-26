using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] string[] tasks;
    [SerializeField] GameObject taskDisplayPrefab;
    public TaskDisplay[] taskDisplays;

    float endTimer = -1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        taskDisplays = new TaskDisplay[tasks.Length];
        for (int i = 0; i < tasks.Length; i++) 
        {
            GameObject newTask = Instantiate(taskDisplayPrefab, canvas.transform);
            newTask.transform.position += 30 * Vector3.down * i;
            newTask.GetComponent<TextMeshProUGUI>().text = tasks[i];
            taskDisplays[i] = newTask.GetComponent<TaskDisplay>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endTimer != -1f)
        {
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                EndLevel();
            }
        }
    }

    public void UpdateTasks()
    {
        bool allcomplete = true;
        foreach (TaskDisplay display in taskDisplays)
        {
            allcomplete = allcomplete && display.completed;
        }
        if (allcomplete) 
        {
            endTimer = 1;
        }
    }

    private void EndLevel()
    {
        SceneManager.LoadScene("LevelComplete");
    }
}
