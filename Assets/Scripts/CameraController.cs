using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        if (players.Length == 0)
        { 
            return;
        }

        Vector3 boundaryStart = players[0].transform.position;
        Vector3 boundaryEnd = players[0].transform.position;
        foreach (GameObject player in players)
        {
            if (player.transform.position.x < boundaryStart.x)
            {
                boundaryStart.x = player.transform.position.x;
            }
            if (player.transform.position.y < boundaryStart.y)
            {
                boundaryStart.y = player.transform.position.y;
            }
            if (player.transform.position.z < boundaryStart.z)
            {
                boundaryStart.z = player.transform.position.z;
            }
            if (player.transform.position.x > boundaryEnd.x)
            {
                boundaryEnd.x = player.transform.position.x;
            }
            if (player.transform.position.y > boundaryEnd.y)
            {
                boundaryEnd.y = player.transform.position.y;
            }
            if (player.transform.position.z > boundaryEnd.z)
            {
                boundaryEnd.z = player.transform.position.z;
            }
        }
        float distance = Mathf.Clamp(((boundaryStart - boundaryEnd).magnitude + 8) * 0.8f, 10, 20);
        transform.position = (boundaryStart + boundaryEnd) / 2 - distance * transform.forward;
    }
}
