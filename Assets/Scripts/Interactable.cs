using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public PlayerMovement playerMovement;
    PlayerInput playerInput;
    InputAction interactAction;
    public Boolean isTouching = false;
    public Boolean isInteracting = false;
    [SerializeField] GameObject player;
    
    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        playerMovement = player.GetComponent<PlayerMovement>(); 
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

        if (isInteracting)
        {
            playerMovement.enabled = false;
        }
        else
        {
            playerMovement.enabled = true;
        }
    }

}
