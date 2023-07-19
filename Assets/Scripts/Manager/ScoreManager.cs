using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    static public Text scoreText;
    static public int score;
    private void Start()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        score = 0;
        scoreText.text = "0";
    }

    static public void ScoreUp(int plusScore)
    {
        score += plusScore;
        scoreText.text = score.ToString();
    }
}
