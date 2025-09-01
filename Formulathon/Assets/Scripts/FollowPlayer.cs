using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;
    public Vector3 offset;
    public Vector3 menu;
    public Quaternion menuRotation;



    public float floatAmount = 0.01f; // Amount of floating
    public float floatInterval = 2f; // Time interval for floating up/down

    public float panRange = 2f; // Maximum distance to pan
    public float panInterval = 2f; //Time interval for panning 

    public float phaseShift = 0.0f; // phase shift

    private float startYPos;
    private float startYRot;

    [SerializeField] private float transitionTime = 1.5f; // how long the move should take
    private float transitionProgress = 0f;


    public enum CameraState
    {
        Menu,
        Cinematic,
        Gameplay
    }

    public CameraState state = CameraState.Menu;

    private void Start()
    {
        // doing this so the camera's origin is clamped to its original y position/rotation.
        startYPos = transform.position.y;
        startYRot = transform.rotation.eulerAngles.y;
    }

    private void Update()
    {
        switch (state)
        {
            case CameraState.Menu:
                // Calculate the new Y position based on the sine wave
                float newYPos = floatAmount * Mathf.Sin(Time.time * floatInterval + phaseShift);
                float newYRot = panRange * Mathf.Sin(Time.time * panInterval + phaseShift);

                // Update the position of the GameObject
                transform.position = new Vector3(transform.position.x, startYPos + newYPos, transform.position.z);
                transform.rotation = Quaternion.Euler(transform.rotation.x, startYRot + newYRot, transform.rotation.z);
                break;
            
            case CameraState.Cinematic:

                if (transitionProgress < 1f)
                {
                    transitionProgress += Time.deltaTime / transitionTime;

                    transform.position = Vector3.Lerp(menu, playerPos.position + offset, transitionProgress);
                    transform.rotation = Quaternion.Lerp(menuRotation, Quaternion.identity, transitionProgress);
                }
                else
                {
                    state = CameraState.Gameplay;
                    GameManager.instance.StartGame();
                }

                    break;
            
            case CameraState.Gameplay:
                transform.position = playerPos.position + offset;
                break;
        }
    }
}
