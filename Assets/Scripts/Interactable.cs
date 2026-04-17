using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction interactAction;
    public Boolean isTouching = false;
    public Boolean isInteracting = false;
    [SerializeField] GameObject player;
    
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];   
    }

    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerCollider")
        {
            isTouching = true;
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag ==  "PlayerCollider")
        {
            isTouching = false;
        }
    }
  
    void Update()
    {
        if (isTouching && interactAction.IsPressed())
            {
                isInteracting = true;
            }
        else
            {
                isInteracting = false;
            }
    }

}
