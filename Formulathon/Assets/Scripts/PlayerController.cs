using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource explosionSFX;
    [SerializeField] private Lives livesCounter;

    public float strafeForce = 500f;
    public float turnSpeed = 100f;
    public float turnAngle = 45f;

    public enum Direction
    {
        Left = -1,
        Right = 1,
        Straight = 0
    }
    private int currentDirection = 0;


    public int lives = 3;
    public bool isDead = false;

    public ParticleSystem explosion;

    private void Start()
    {
        enabled = false;
    }

    void FixedUpdate()
    {
        /*int direction = 0;
        if (Input.GetKey("d")) direction = 1;
        if (Input.GetKey("a")) direction = -1; //cant use get axis since for whatever reason it makes the car less responsive*/

        if (currentDirection != 0)
        {
            rb.AddForce(currentDirection * strafeForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            float newY = Mathf.MoveTowardsAngle(transform.eulerAngles.y, turnAngle * currentDirection, turnSpeed * Time.deltaTime);
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

    public void Turn(Direction direction)
    {
        currentDirection = (int)direction;
    }

    public void Die()
    {
        explosion.Play();
        explosionSFX.Play();
        enabled = false;
        isDead = true;
        lives -= 1;
        livesCounter.UpdateLivesCounter();
    }

    public void Respawn()
    {
        enabled = true;
        isDead = false;
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
