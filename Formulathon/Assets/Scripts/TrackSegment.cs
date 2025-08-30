using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.Translate(Vector3.back * GameManager.instance.worldSpeedCURRENT * Time.deltaTime);
    }
}
