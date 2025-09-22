using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Invoker : MonoBehaviour
{
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;
    private float _maxRecordingTime = 3f;
    private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();
    private SortedList<float, Vector3> _positions = new SortedList<float, Vector3>();
    private SortedList<float, Quaternion> _rotations = new SortedList<float, Quaternion>();

    public void ExecuteCommand(Command command)
    {
        command.Execute();

        if (_isRecording)
        {
            // Check if the key already exists
            if (!_recordedCommands.ContainsKey(_recordingTime))
            {
                // If the key does not exist, add the command to the list
                _recordedCommands.Add(_recordingTime, command);
                _positions.Add(_recordingTime, GameManager.instance.player.transform.position);
                _rotations.Add(_recordingTime, GameManager.instance.player.transform.rotation);
            }
            else
            {
                // Optionally, update the command at this time (if you want to replace it)
                _recordedCommands[_recordingTime] = command;
            }

            // Find keys to remove (older than the time threshold)
            float timeThreshold = _recordingTime - _maxRecordingTime;
            var keysToRemove = _recordedCommands.Keys.Where(k => k < timeThreshold).ToList();
            foreach (var key in keysToRemove)
            {
                _recordedCommands.Remove(key);
                _positions.Remove(key);
                _rotations.Remove(key);
            }
        }
            

        //Debug.Log("Recorded Time: " + _recordingTime);
        //Debug.Log("Recorded Command: " + command);
    }

    public void Record()
    {
        _recordingTime = 0.0f;
        _isRecording = true;
    }

    public void Replay()
    {
        _replayTime = _recordingTime - _maxRecordingTime;
        _isReplaying = true;

        if (_recordedCommands.Count <= 0)
            Debug.LogError("No commands to replay!");
    }

    public Vector3 GetReplayStartPosition()
    {
        if (_positions.Any())
        {
            if (_replayTime >= _positions.Keys[0])
            {
                return _positions.Values[0];
            }
        }

        return Vector3.zero;
    }

    public Quaternion GetReplayStartRotation()
    {
        if (_rotations.Any())
        {
            if (_replayTime >= _rotations.Keys[0])
            {
                return _rotations.Values[0];
            }
        }

        return Quaternion.identity;
    }

    void FixedUpdate()
    {
        if (_isRecording)
        {
            _recordingTime += Time.fixedDeltaTime;
        }
            

        if (_isReplaying)
        {
            _replayTime += Time.deltaTime;

            if (_recordedCommands.Any())
            {
                if (_replayTime >= _recordedCommands.Keys[0])
                {

                    Debug.Log("Replay Time: " + _replayTime);
                    Debug.Log("Replay Command: " + _recordedCommands.Values[0]);

                    _recordedCommands.Values[0].Execute();
                    _recordedCommands.RemoveAt(0);
                }
            }
            else
            {
                _isReplaying = false;
                GameManager.instance.StartRespawnSequence();
            }
        }
    }
}
