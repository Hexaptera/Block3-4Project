using UnityEngine;
using UnityEngine.UI;

public class TaskDisplay : MonoBehaviour
{
    [SerializeField] Image TickBox;

    public bool completed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Complete()
    {
        TickBox.color = Color.green;
        completed = true;
    }
}
