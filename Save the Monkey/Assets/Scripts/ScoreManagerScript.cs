using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public float score = 0f;

    void FixedUpdate()
    {
        score += Time.deltaTime;
        if (score> PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", (int)score);
        }

        scoreText.text = score.ToString("0");
    }
}
