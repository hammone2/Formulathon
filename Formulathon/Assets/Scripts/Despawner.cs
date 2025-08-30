using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Track")
        {
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
    }
}
