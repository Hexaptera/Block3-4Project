using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFotographing : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction lookAction;
    InputAction lookVerticalAction;
    InputAction cameraToggleAction;
    bool cameraActive = false;

    [SerializeField] GameObject photoCamera;
    [SerializeField] GameObject viewVisual;
    [SerializeField] GameObject eyes;

    [SerializeField] float verticalViewAngle;
    [SerializeField] float displayHeight;
    [SerializeField] Vector2 displaySize;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Look"];
        lookVerticalAction = playerInput.actions["Look Vertical"];
        cameraToggleAction = playerInput.actions["Interact"];
    }

    void Update()
    {
        if (cameraToggleAction.WasPressedThisFrame()) cameraActive = !cameraActive;
        if (cameraActive)
        {
            photoCamera.SetActive(true);
            viewVisual.SetActive(true);

            Vector2 look = lookAction.ReadValue<Vector2>();
            if (look.magnitude > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(look.x, 0, look.y), new Vector3(0, 1, 0)), 0.05f);
            }

            eyes.transform.localRotation = Quaternion.Lerp(eyes.transform.localRotation, Quaternion.Euler(new Vector3(lookVerticalAction.ReadValue<float>() * 20, 0, 0)), 0.1f);

            Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, displayHeight, 0)) / new Vector2(Screen.width, Screen.height) - displaySize / 2;
            photoCamera.GetComponent<Camera>().rect = new Rect(displayPos, displaySize);
            
        } 
        else
        {
            photoCamera.SetActive(false);
            viewVisual.SetActive(false);
        }
    }
}
