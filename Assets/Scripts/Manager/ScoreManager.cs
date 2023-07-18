using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public int score;
    private void Start()
    {
        score = 0;
        scoreText.text = "0";
    }

    public void ScoreUp(int plusScore)
    {
        score += plusScore;
        scoreText.text = score.ToString();
    }
}
