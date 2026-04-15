using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float damping;
    [SerializeField] GameObject eyes;
    [SerializeField] public LayerMask animalAreaMask;

    Rigidbody rigidBody;
    float directionChangeTimer;
    Quaternion direction;

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

        transform.rotation = Quaternion.Lerp(transform.rotation, direction, 0.03f);
        rigidBody.linearVelocity = transform.forward * speed;
    }

    void ChangeDirection()
    {
        direction *= Quaternion.Euler(0, Random.Range(-60, 60), 0);
        directionChangeTimer = 0.7f;
    }
}
