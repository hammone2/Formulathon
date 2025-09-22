using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Invoker _invoker;
    private bool _isReplaying;
    private bool _isRecording;
    private PlayerController _playerController;
    private Command _buttonA, _buttonD, _buttonNone;

    void Start()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _playerController = FindFirstObjectByType<PlayerController>();

        _buttonA = new TurnLeft(_playerController);
        _buttonD = new TurnRight(_playerController);
        _buttonNone = new GoStraight(_playerController);
    }

    void Update()
    {
        if (!_isReplaying && _isRecording)
        {
            if (Input.GetKey(KeyCode.A))
                _invoker.ExecuteCommand(_buttonA);
            else if (Input.GetKeyUp(KeyCode.A))
                _invoker.ExecuteCommand(_buttonNone);

            if (Input.GetKey(KeyCode.D))
                _invoker.ExecuteCommand(_buttonD);
            else if (Input.GetKeyUp(KeyCode.D))
                _invoker.ExecuteCommand(_buttonNone);
        }
    }

    public void StartRecording()
    {
        _isReplaying = false;
        _isRecording = true;
        _invoker.Record();
    }

    public void StopRecording()
    {
        _isRecording = false;
    }

    public void StartReplay()
    {
        _isReplaying = true;
        _isRecording = false;
        _invoker.Replay();
    }

    public Vector3 GetReplayStartPosition()
    {
        return _invoker.GetReplayStartPosition();
    }

    public Quaternion GetReplayStartRotation()
    {
        return _invoker.GetReplayStartRotation();
    }
}
