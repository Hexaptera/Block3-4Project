using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    TextMeshProUGUI textMesh;
    public int score;
    float time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();   
    }

    // Update is called once per frame
    void Update()
    {
        score += 1;
        time += Time.deltaTime;
        textMesh.text = $"Score:{score}\nTime: {Math.Round(time, 2)}";
    }
}
