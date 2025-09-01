using UnityEngine;
using TMPro;
using System.Collections;

public class CountdownUI : MonoBehaviour
{
    public TextMeshProUGUI countdownText;

    public IEnumerator CountdownRoutine(float countdownTime)
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}