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
            }
            else
            {
                // Optionally, update the command at this time (if you want to replace it)
                _recordedCommands[_recordingTime] = command;
            }


            float timeThreshold = _recordingTime - _maxRecordingTime;

            // Find keys to remove (older than the time threshold)
            var keysToRemove = _recordedCommands.Keys.Where(k => k < timeThreshold).ToList();

            // Remove the outdated commands
            foreach (var key in keysToRemove)
            {
                _recordedCommands.Remove(key);
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
        _replayTime = _recordingTime - _maxRecordingTime; //0.0f;
        _isReplaying = true;

        if (_recordedCommands.Count <= 0)
            Debug.LogError("No commands to replay!");

        //_recordedCommands.Reverse();
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
                if (_replayTime >= _recordedCommands.Keys[0]) //(Mathf.Approximately(_replayTime, _recordedCommands.Keys[0]))
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
