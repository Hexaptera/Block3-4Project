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
    InputAction cameraToggleAction;
    public bool cameraActive = false;

    [SerializeField] GameObject photoCamera;
    [SerializeField] GameObject viewVisual;
    [SerializeField] GameObject eyes;

    [SerializeField] float verticalViewAngle;
    [SerializeField] float displayHeight;
    [SerializeField] Vector2 displaySize;

    [SerializeField] GameObject scoreDisplay;
    [SerializeField] Material redMaterial;
    [SerializeField] Material greenMaterial;
    [SerializeField] Material blueMaterial;

    List<GameObject> seenAnimals = new List<GameObject>();

    int score = 0;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        lookAction = playerInput.actions["Move"];
        lookVerticalAction = playerInput.actions["Look Vertical"];
        cameraToggleAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        //if (cameraToggleAction.WasPressedThisFrame()) cameraActive = !cameraActive;
        cameraActive = cameraToggleAction.IsPressed();

        if (cameraActive)
        {
            photoCamera.SetActive(true);
            viewVisual.SetActive(true);

            Vector2 look = lookAction.ReadValue<Vector2>();
            if (look.magnitude > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(new Vector3(look.x, 0, look.y), new Vector3(0, 1, 0)), 0.075f);
            }

            eyes.transform.localRotation = Quaternion.Lerp(eyes.transform.localRotation, Quaternion.Euler(new Vector3(lookVerticalAction.ReadValue<float>() * 20, 0, 0)), 0.1f);

            Vector2 displayPos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, displayHeight, 0)) / new Vector2(Screen.width, Screen.height) - displaySize / 2;
            photoCamera.GetComponent<Camera>().rect = new Rect(displayPos, displaySize);
        } 
        else
        {
            foreach (GameObject animalCollider in seenAnimals)
            {
                animalCollider.GetComponent<MeshRenderer>().material = blueMaterial;
                AnimalPhotographing animalPhotographing = animalCollider.transform.parent.GetComponent<AnimalPhotographing>();
                animalPhotographing.photographed = true;
                score += animalPhotographing.points;
            }
            seenAnimals = new List<GameObject>();
            scoreDisplay.GetComponent<TextMeshPro>().text = score.ToString();

            photoCamera.SetActive(false);
            viewVisual.SetActive(false);
        }

        scoreDisplay.transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Animal") && !other.transform.parent.GetComponent<AnimalPhotographing>().photographed)
        {
            other.GetComponent<MeshRenderer>().material = greenMaterial;
            seenAnimals.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent && other.transform.parent.CompareTag("Animal") && !other.transform.parent.GetComponent<AnimalPhotographing>().photographed)
        {
            other.GetComponent<MeshRenderer>().material = redMaterial;
            seenAnimals.Remove(other.gameObject);
        }
    }
}
