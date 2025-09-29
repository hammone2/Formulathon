using TMPro;
using UnityEngine;
using System;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    [SerializeField] HighScore highScoreHandler;

    public float score = 0;
    public float highScore;
    private float runTime = 0f;
    private bool isDisplayingScore = true;
    private bool highScorePassed = false;

    public event Action OnNewHighScore;

    private void Start()
    {
        if (highScoreHandler != null)
        {
            highScoreHandler.OnLoadHighScore += () => LoadHighScore(highScoreHandler.highScore);
        }
    }


    void Update()
    {
        if (!GameManager.instance.gameOver)
            runTime += Time.deltaTime;

        float metersTravelled = GameManager.instance.worldSpeedCURRENT * Time.deltaTime;
        score += metersTravelled;

        if (isDisplayingScore)
            scoreText.text = score.ToString("0") + "m";

        if (!highScorePassed)
        {
            if (score > highScore)
            {
                highScorePassed = true;
                DisplayHighScore();
            }
        }
    }

    public void DisplayHighScore()
    {
        isDisplayingScore = false;
        scoreText.SetText("New High Score!");
        Invoke("ShowNormalScore", 3f);
    }

    private void ShowNormalScore()
    {
        isDisplayingScore = true;
    }

    private void LoadHighScore(float _newHighScore)
    {
        highScore = _newHighScore;
        Debug.Log("High score loaded: " + highScore);
    }

    public void NewHighScore()
    {
        OnNewHighScore?.Invoke();
        highScore = score;
    }
}
