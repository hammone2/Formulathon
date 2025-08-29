using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset;

    private void Update()
    {
        transform.position = playerPos.position + offset;
    }

}
