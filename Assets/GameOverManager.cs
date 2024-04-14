using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private List<GameObject> children;

    public TMP_Text game_over_text;

    private bool canRestart = false;
    

    private void Start()
    {
        children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        SetChildState(false);
        Player.OnGameOver += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        SetChildState(true);
        // Wait for 3 seconds and then transition to restart suggestion
        StartCoroutine(WaitAndTransition());
    }


    void SetChildState(bool state)
    {
        foreach (var child in children)
        {
            child.SetActive(state);
        }
    }

    
    IEnumerator WaitAndTransition()
    {
        yield return new WaitForSeconds(3f);
        TransitionToRestartSuggestion();
    }

    private void TransitionToRestartSuggestion()
    {
        // Dotween Fade out text
        game_over_text.DOFade(0, 1f).OnComplete(() =>
        {
            game_over_text.text = "Tap to Restart";
            game_over_text.DOFade(1, 1f);
        });
    }
    
    
private void Update()
    {
        if (canRestart && Input.GetMouseButtonDown(0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
    
}