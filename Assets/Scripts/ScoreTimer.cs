using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class ScoreTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private int startingScore = 0;
    [SerializeField]
    private int increment = 1;
    [SerializeField]
    private int currentScore = 0;
    [SerializeField]
    private bool timerIsRunning = false;
    [SerializeField]
    private float timeBetweenTicks = 0.25f;
    [SerializeField]
    private bool debugScoring = false;

    // private Timer timer = new Timer(1000);

    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        Score(startingScore);
    }

    public void StartTimer()
    {
        // timer.Start();
        // timer.Elapsed += IncrementScore;
        if (debugScoring)
        {
            Debug.Log("Scoring timer started.");
        }
        if (!timerIsRunning) {
            timerIsRunning = true;
            StartCoroutine(TimerCoroutine());
        }
    }

    public void StopTimer()
    {
        // timer.Stop();
        if (debugScoring)
        {
            Debug.Log("Scoring timer stopped.");
        }
        timerIsRunning = false;
    }

    public void AddScore(int value)
    {
        Score(value);
    }

    // For System.Timer implementation
    // private void IncrementScore(object sender, ElapsedEventArgs e)
    // {
    //     Score(increment);
    // }

    private void IncrementScore()
    {
        Score(increment);
    }

    private void Score(int value)
    {
        currentScore += value;
        UpdateScoreGUI();
        if (debugScoring)
        {
            Debug.Log("Score added.");
        }
    }

    private void UpdateScoreGUI()
    {
        score.text = currentScore.ToString();
    }

    private IEnumerator TimerCoroutine()
    {
        while (timerIsRunning)
        {
            yield return new WaitForSeconds(timeBetweenTicks);

            IncrementScore();
        }
    }
}
