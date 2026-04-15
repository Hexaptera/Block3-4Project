using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rigidBody;
    PlayerInput playerInput;
    InputAction moveAction;
    InputAction jumpAction;

    [SerializeField] GameObject feet;
    [SerializeField] LayerMask floorCollisionMask;

    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float velocityDamping;
    [SerializeField] float gravity;
    [SerializeField] float jumpStrength;

    public bool inBush = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Interact"];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = moveAction.ReadValue<Vector2>();
        var vel = rigidBody.linearVelocity;

        if (vel.magnitude < speed && !GetComponent<PlayerFotographing>().cameraActive)
        {
            vel += (new Vector3(move.x, 0, move.y) * acceleration * Time.deltaTime);
        }

        bool hit = Physics.Raycast(feet.transform.position, new Vector3(0, -1, 0), 0.1f, floorCollisionMask);
        if (jumpAction.WasPressedThisFrame() && hit)
        {
            vel.y = jumpStrength;
        }
        else
        {
            vel.y -= gravity * Time.deltaTime;
        }

        vel.x *= Mathf.Pow(velocityDamping, Time.deltaTime);
        vel.z *= Mathf.Pow(velocityDamping, Time.deltaTime);
        rigidBody.linearVelocity = vel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bush"))
        {
            inBush = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bush"))
        {
            inBush = false;
        }
    }
}
