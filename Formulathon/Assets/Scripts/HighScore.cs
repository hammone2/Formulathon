using UnityEngine;
using System;

public class HighScore : MonoBehaviour
{
    private const string key = "HighScore";
    public float highScore;
    private bool highScorePassed = false;

    [SerializeField] Score scoreHandler;

    public event Action OnLoadHighScore;

    private bool scoreLoaded = false;

    private void Update()
    {
        if (scoreHandler.isActiveAndEnabled && scoreLoaded == false)
        {
            scoreHandler.OnNewHighScore += () => SaveHighScore(scoreHandler.score);
            LoadHighScore();
            scoreLoaded = true;
        }
    }

    private void SaveHighScore(float _highScore)
    {
        highScore = _highScore;
        PlayerPrefs.SetFloat(key, highScore);
        PlayerPrefs.Save();
        Debug.Log("High score saved: " + highScore);
    }

    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat(key, 0 /*default*/);
        OnLoadHighScore?.Invoke();
    }
}
