using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject stuffToAppear;
    [SerializeField] private AudioSource gameOverMusic;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;

    private void OnEnable()
    {
        stuffToAppear.SetActive(false);
        gameOverMusic.Play();
    }

    public void LoadButton()
    {
        stuffToAppear.SetActive(true);
        finalScoreText.text = "Final Score: " + scoreText.text;
    }
}
