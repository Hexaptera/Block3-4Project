using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float acceleration;
    [SerializeField] float damping;

    Rigidbody rigidBody;
    GameObject currentPoint;
    float pauseTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        PickPoint();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentPoint)
        {
            Debug.DrawLine(transform.position, currentPoint.transform.position);
            Vector3 difference = (currentPoint.transform.position - transform.position);
            if (rigidBody.linearVelocity.magnitude < speed)
            {
                rigidBody.linearVelocity += difference.normalized * acceleration * Time.deltaTime;
            }
            rigidBody.linearVelocity *= Mathf.Pow(damping, Time.deltaTime);

            if (difference.magnitude < 1)
            {
                currentPoint = null;
                pauseTimer = Random.Range(0.0f, 5.0f);
            }
        }
        else
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer < 0.0f)
            {
                PickPoint();
            }
        }
    }

    void PickPoint()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("Animal Point");
        currentPoint = points[Random.Range(0, points.Length)];
    }
}
