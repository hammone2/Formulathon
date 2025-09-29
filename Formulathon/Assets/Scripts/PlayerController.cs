using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, ICarElement
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource explosionSFX;
    [SerializeField] private Lives livesCounter;

    public float strafeForce = 500f;
    public float turnSpeed = 100f;
    public float turnAngle = 45f;

    private List<ICarElement> _carElements = new List<ICarElement>();

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

        _carElements.Add(gameObject.AddComponent<InvincibilityShield>());
        _carElements.Add(gameObject.AddComponent<BonusLife>());
    }

    public void Accept(IVisitor visitor)
    {
        foreach (ICarElement element in _carElements)
        {
            element.Accept(visitor);
        }
    }

    void FixedUpdate()
    {
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
        livesCounter.UpdateLivesCounter(lives);
    }

    public void Respawn()
    {
        enabled = true;
        isDead = false;
        transform.position = new Vector3(0.0f, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void SetReplayStartPos(Vector3 _position, Quaternion _rotation)
    {
        transform.position = _position;
        transform.rotation = _rotation;
    }

    public void UpdateLives()
    {
        lives += 1;
        livesCounter.UpdateLivesCounter(lives);
    }
}
