using System;
using UnityEngine;

public class GarbageInteract : MonoBehaviour
{

    public bool isInteracting;
    public Interactable Interactable;
    void Update()
    {
        isInteracting = Interactable.isInteracting;
        if(isInteracting)
        {
            Destroy(gameObject);
        }
    }
}
