using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GrassBedPart : MonoBehaviour
{

    public bool isTouching;
    public Interactable Interactable;
    MeshRenderer render;
    MeshRenderer otherRender;
    bool isInGrass;

    void Start()
    {
        Interactable = GetComponent<Interactable>();
        render = GetComponent<MeshRenderer>();
    }

        void OnTriggerEnter(Collider other)
    {
        otherRender = other.GetComponent<MeshRenderer>();
        if(other.tag == "Animal")
        {
            isInGrass = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        otherRender = other.GetComponent<MeshRenderer>();
        if(other.tag == "Animal")
        {
            isInGrass = false;
        }
    }

    void Update()
    {
        isTouching = Interactable.isTouching;
        if(isTouching)
        {
            render.enabled = false;
        }
        else
        {
            render.enabled = true;
        }

        if(render.enabled && isInGrass)
        {
            otherRender.enabled = false;
        }
        else
        {
             otherRender.enabled = true;
        }
        
    }
}
