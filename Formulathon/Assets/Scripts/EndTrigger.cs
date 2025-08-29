using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.CompleteLevel();
    }
}
