using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float strafeForce = 500f;
    public float turnSpeed = 100f;
    public float turnAngle = 45f;

    public int lives = 3;
    public bool isDead = false;

    public ParticleSystem explosion;

    private void Start()
    {
        enabled = false;
    }

    void FixedUpdate()
    {
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
            explosion.Play();
            GameManager.instance.EndGame();
        }
    }

    public void Die()
    {
        explosion.Play();
        enabled = false;
        isDead = true;
        lives -= 1;
    }

    public void Respawn()
    {
        enabled = true;
        isDead = false;
    }
}
