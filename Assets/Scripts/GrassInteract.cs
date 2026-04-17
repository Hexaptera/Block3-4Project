using System;
using UnityEngine;

public class GrassInteract : MonoBehaviour
{

    public bool isInteracting;
    public Interactable Interactable;
    MeshRenderer render;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        isInteracting = Interactable.isInteracting;
        if(isInteracting)
        {
            render.enabled = !render.enabled;
        }
        else
        {
            render.enabled = render.enabled;
        }
    }
}
