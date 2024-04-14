using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    private List<GameObject> children;
    
    

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
    }
    
    
    void SetChildState(bool state)
    {
        foreach (var child in children)
        {
            child.SetActive(state);
        }
    }
    
    
}
