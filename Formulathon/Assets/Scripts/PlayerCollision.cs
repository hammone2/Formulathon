using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController controller;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Car")
        {
            if (controller.isDead == true)
                return;

            controller.Die();

            if (controller.lives <= 0)
                GameManager.instance.EndGame();
            else
                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());  
        }
    }
}
