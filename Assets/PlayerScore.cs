using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : StaticInstance<PlayerScore>
{
    TMP_Text scoreText;
    public int score = 0;

    String scoretxt
    {
        get { return String.Format("Score: {0:00}", score); }
    }


    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = scoretxt;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = scoretxt;
    }
}