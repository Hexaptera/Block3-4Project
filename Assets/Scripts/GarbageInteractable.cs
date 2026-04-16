using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction interactAction;
    Boolean isTouching = false;
    [SerializeField] GameObject player;
    [SerializeField] LayerMask playerLayer;

    void Start()
    {
        playerInput = player.GetComponent<PlayerInput>();
        interactAction = playerInput.actions["Interact"];

    }

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag ==  "Player")
        {
            isTouching = true;
            
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag ==  "Player")
        {
            isTouching = false;
        }
    }
  
    void Update()
    {
        Debug.Log(isTouching);
        if (isTouching && interactAction.IsPressed())
            {
                Destroy(gameObject); 
            }
    }
}
