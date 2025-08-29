using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float forwardForce = 2000f;
    public float strafeForce = 500f;
    public float turnSpeed = 100f;
    public float turnAngle = 45f;
    public float minumumSpeed = 25f;
    public float brakingForce = 10f;

    void FixedUpdate()
    {
        /*rb.AddForce(0, 0, forwardForce * Time.deltaTime);
        if (Input.GetKey("space")) //braking
        {
            if (rb.linearVelocity.z > minumumSpeed)
                rb.AddForce(0, 0, -rb.linearVelocity.z * brakingForce, ForceMode.Acceleration);
        }*/

        int direction = 0;
        if (Input.GetKey("d")) direction = 1;
        if (Input.GetKey("a")) direction = -1; //cant use get axis since for whatever reason it makes the car less responsive

        if (direction != 0)
        {
            rb.AddForce(direction * strafeForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, turnAngle * direction, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);
        }
        else
        {
            float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, 0f, turnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, newY, transform.rotation.z);
        }

        // end game if the player falls off
        if (rb.position.y <= -3)
        {
            GameManager.instance.EndGame();
        }
    }
}
