using UnityEngine;
using System;

public class Pickup : MonoBehaviour
{
    public PowerUp powerup;
    public event Action OnPickedUp;
    public float minSpeed = 25f;
    public float maxSpeed = 60f;
    private GameObject model;
    private float speed;

    [SerializeField] Rigidbody rb;

    private void Start()
    {
        model = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        model.transform.Rotate(0, 0, 360f * Time.deltaTime);
        Vector3 movement = new Vector3(0, 0, -1) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            other.GetComponent<PlayerController>().Accept(powerup);
            OnPickedUp?.Invoke();
        }
    }
}