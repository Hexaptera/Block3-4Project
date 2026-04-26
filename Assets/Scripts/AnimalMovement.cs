using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float damping;
    [SerializeField] GameObject eyes;
    [SerializeField] public LayerMask animalAreaMask;
    List<GameObject> objectsToAvoid = new List<GameObject>();

    Rigidbody rigidBody;
    float directionChangeTimer;
    Quaternion direction;
    public GameObject currentFood;
    float foodTimer;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        direction = transform.rotation;
    }
    void Update()
    {
        if (!Physics.Raycast(eyes.transform.position, direction * eyes.transform.localRotation * Vector3.forward, Mathf.Infinity, animalAreaMask))
        {
            Debug.DrawRay(eyes.transform.position, direction * eyes.transform.localRotation * Vector3.forward, Color.red);
            ChangeDirection();
        } else
        {
            Debug.DrawRay(eyes.transform.position, direction * eyes.transform.localRotation * Vector3.forward, Color.green);
        }

            directionChangeTimer -= Time.deltaTime;
        if (directionChangeTimer < 0.0f)
        {
            ChangeDirection();
        }

        if (currentFood != null)
        {
            TargetPosition(currentFood.transform.position);
            foodTimer -= Time.deltaTime;
            if (foodTimer < 0.0f)
            {
                GameObject.Destroy(currentFood);
                currentFood = null;
            }
        }

        foreach (GameObject gameObject in objectsToAvoid)
        {
            if (gameObject != null) {
                if (!gameObject.CompareTag("PlayerCollider") || !gameObject.transform.parent.GetComponent<PlayerMovement>().inBush)
            { 
                AvoidPosition(gameObject.transform.position);
            }
            } else
            {
               objectsToAvoid.Remove(gameObject);
            }
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, direction, 1 - Mathf.Exp(-10f * Time.deltaTime));
        rigidBody.linearVelocity = transform.forward * speed;
    }

    void ChangeDirection()
    {
        direction *= Quaternion.Euler(0, Random.Range(-60, 60), 0);
        directionChangeTimer = 0.7f;
    }
    void AvoidPosition(Vector3 position)
    {
        direction = Quaternion.Lerp(direction, Quaternion.LookRotation(Vector3.Scale((transform.position - position), new Vector3(1, 0, 1))), 1 - Mathf.Exp(-10f * Time.deltaTime));
        directionChangeTimer = 0.7f;
    }

    void TargetPosition(Vector3 position)
    {
        direction = Quaternion.LookRotation(Vector3.Scale((position - transform.position), new Vector3(1, 0, 1)));
        directionChangeTimer = 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Garbage" || gameObject.tag == "PlayerCollider")
        {
            objectsToAvoid.Add(gameObject);
            //Debug.Log(gameObject.name);
        }
        else if (gameObject.tag == "Food")
        {
            currentFood = gameObject;
            foodTimer = 4;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject gameObject = other.gameObject;
        if (gameObject.tag == "Garbage" || gameObject.tag == "PlayerCollider")
        {
            objectsToAvoid.Remove(gameObject);
        }
    }
}
