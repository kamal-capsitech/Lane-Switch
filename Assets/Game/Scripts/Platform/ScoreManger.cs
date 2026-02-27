using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int starsCollected = 0;
    public float score = 0;

    public Text gamescoreText;

    public Text resultscoreText;
    public Text highscoreText;
    public Text starcountText;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateScore(string time)
    {
        if (!GameManager.IsGameStart)
        {
            return;
        }

        gamescoreText.text = "" + time;

        if (float.TryParse(time, out float parsedScore))
        {
            score = parsedScore;
        }
        else
        {
            score = 0;
        }
    }

    public void AddStar()
    {
        starsCollected++;
        Debug.Log("Stars: " + starsCollected);
        starcountText.text = "" + starsCollected;
    }

    public void ResetScore()
    {
        starsCollected = 0;
        score = 0;
        gamescoreText.text = "" + score;
        starcountText.text = "" + starsCollected;
    }
    
    public void HighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetFloat("HighScore", score);
            Debug.Log("New High Score: " + score);
        }
        highscoreText.text = "" + PlayerPrefs.GetInt("HighScore", 0);
        resultscoreText.text = "" + score;
    }
}