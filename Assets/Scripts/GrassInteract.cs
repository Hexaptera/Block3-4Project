using System;
using UnityEngine;

public class GrassInteract : MonoBehaviour
{

    public bool isInteracting;
    public Interactable Interactable;
    MeshRenderer render;
    CapsuleCollider grassCollider;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
        Interactable = GetComponent<Interactable>();
        grassCollider = GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        isInteracting = Interactable.isInteracting;
        if(isInteracting)
        {
            render.enabled = false;
            grassCollider.enabled = false;
        }
        else
        {
            render.enabled = true;
            grassCollider.enabled = true;
        }
    }
}
