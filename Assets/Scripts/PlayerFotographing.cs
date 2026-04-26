using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerFotographing : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction lookAction;
    InputAction lookVerticalAction;
    InputAction activateCameraAction;
    public bool cameraActive = false;

    [SerializeField] GameObject photoCamera;
    [SerializeField] GameObject viewVisual;
    [SerializeField] GameObject eyes;

    [SerializeField] float verticalViewAngle;
    [SerializeField] float displayHeight;
    [SerializeField] Vector2 displaySize;

    [SerializeField] float maxPhotoDistance;
    [SerializeField] float maxPhotoFOV;

    [SerializeField] GameObject scoreDisplay;
    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material blueMaterial;

    int score = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Move"];
        lookVerticalAction = playerInput.actions["Look Vertical"];
        activateCameraAction = playerInput.actions["Activate Camera"];
    }

    void Update()
    {
        //if (cameraToggleAction.WasPressedThisFrame()) cameraActive = !cameraActive;
        cameraActive = activateCameraAction.IsPressed();
        photoCamera.SetActive(cameraActive);
        viewVisual.SetActive(cameraActive);

        if (cameraActive)
        {
            viewVisual.transform.localScale = new Vector3(maxPhotoDistance, maxPhotoDistance, maxPhotoDistance) * 50;

            Vector2 look = lookAction.ReadValue<Vector2>();
            if (look.magnitude > 0)
            {
                Transform cam = Camera.main.gameObject.transform;
                Vector3 lookDirection = Quaternion.FromToRotation(cam.up, Vector3.up) * (look.x * cam.right + look.y * cam.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection, new Vector3(0, 1, 0)), 1 - Mathf.Exp(-10f * Time.deltaTime));
            }

            eyes.transform.localRotation = Quaternion.Lerp(eyes.transform.localRotation, Quaternion.Euler(new Vector3(lookVerticalAction.ReadValue<float>() * verticalViewAngle, 0, 0)), 1 - Mathf.Exp(-20f * Time.deltaTime));

            Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, displayHeight, 0)) / new Vector2(Screen.width, Screen.height) - displaySize / 2;
            photoCamera.GetComponent<Camera>().rect = new Rect(displayPos, displaySize);
        } 
        else if (activateCameraAction.WasReleasedThisFrame())
        {
            foreach (GameObject animal in GameObject.FindGameObjectsWithTag("Animal"))
            {
                Vector3 eyesToAnimal = animal.transform.position - eyes.transform.position;
                if (eyesToAnimal.magnitude < maxPhotoDistance + 0.2f && Vector3.Angle(eyesToAnimal, eyes.transform.forward) < maxPhotoFOV)
                {
                    GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager>().taskDisplays[0].Complete();
                    score += 1;
                }
            }
            scoreDisplay.GetComponent<TextMeshPro>().text = score.ToString();
        }
        scoreDisplay.transform.rotation = Camera.main.transform.rotation;
    }
}