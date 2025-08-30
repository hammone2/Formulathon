using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float minSpeed = 25f;
    public float maxSpeed = 60f;
    public float turnSpeed = 100f;
    public float turnAngle = 45f;

    private float speed;
    private int direction = 0;

    [SerializeField] private Rigidbody rb;

    private void OnEnable()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        StartTurning();
    }

    private void FixedUpdate()
    {
        rb.AddForce(0,0,-1 * speed * 10 * Time.deltaTime);

        if (direction != 0)
        {
            rb.AddForce(direction * speed/3 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, turnAngle * direction, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);
        }
        else
        {
            float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, 0f, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);
        }
    }

    private void StartTurning()
    {
        StartCoroutine(TurnRandomly(Random.Range(0.1f, 1f)));
    }

    IEnumerator TurnRandomly(float timer)
    {
        int[] directions = { 1, -1 };
        direction = directions[Random.Range(0, directions.Length)];

        yield return new WaitForSeconds(timer);

        direction = 0;
        StartCoroutine(TurnCooldown(Random.Range(0.1f, 2f)));
    }

    IEnumerator TurnCooldown(float timer)
    {
        yield return new WaitForSeconds(timer);

        StartTurning();
    }
}