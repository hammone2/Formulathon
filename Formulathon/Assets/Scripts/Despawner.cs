using Unity.VisualScripting;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    public Pickup[] powerUps;

    void Start()
    {
        // Subscribe to each power-up's event
        foreach (var powerUp in powerUps)
        {
            if (powerUp != null)
            {
                powerUp.OnPickedUp += () => DespawnPowerUp(powerUp.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Track")
        {
            if (obj.name == "Start")
                Destroy(obj);

            obj.transform.parent = GameManager.instance.trackObjectPool;
            obj.transform.position = GameManager.instance.trackObjectPool.position;
            obj.SetActive(false);
            return;
        }

        if (obj.tag == "Car")
        {
            obj.transform.parent = GameManager.instance.carPool;
            obj.transform.position = GameManager.instance.carPool.position;
            obj.SetActive(false);
            return;
        }

        if (obj.tag == "Prop")
        {
            //obj.transform.parent = GameManager.instance.trackObjectPool;
            return;
        }

        if (obj.tag == "PowerUp")
        {
            DespawnPowerUp(obj);
            return;
        }
    }

    public void DespawnPowerUp(GameObject obj)
    {
        obj.transform.parent = GameManager.instance.carPool;
        obj.transform.position = GameManager.instance.carPool.position;
        obj.SetActive(false);
    }
}
