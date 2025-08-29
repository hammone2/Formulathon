using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController controller;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            controller.enabled = false;
            GameManager.instance.EndGame();
        }
    }
}
