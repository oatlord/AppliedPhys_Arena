using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int scoreCount = 0;
    public void AddScore()
    {
        scoreCount++;
        scoreText.text = "Score: " + scoreCount.ToString();
        Debug.Log("Score Added!");
    }
}