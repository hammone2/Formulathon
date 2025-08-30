using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private float score = 0;
    private float runTime = 0f;

    void Update()
    {
        if (!GameManager.instance.gameOver)
            runTime += Time.deltaTime;

        float metersTravelled = GameManager.instance.worldSpeedCURRENT * Time.deltaTime;
        score += metersTravelled;
        scoreText.text = score.ToString("0") + "m";
    }
}
