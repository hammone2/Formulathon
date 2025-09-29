using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    public PlayerController controller;
    public UnityEvent OnGameOver;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Car")
        {
            if (controller.isDead == true)
                return;

            controller.Die();

            if (controller.lives <= 0)
            {
                GameManager.instance.EndGame();
                OnGameOver?.Invoke();
            }    
            else
                GameManager.instance.StartCoroutine(GameManager.instance.Respawn());  
        }
    }
}
