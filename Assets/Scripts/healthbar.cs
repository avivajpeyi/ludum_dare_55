using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarUI : Singleton<PlayerHealthbarUI> {
    private Slider _health;

    void Start() {
        _health = GetComponent<Slider>();
        _health.minValue = 0f;
        _health.maxValue = 100f;
        _health.value = 100f; // Health bar starts full
        
        // Subscribe to player damage event
        Player.OnPlayerTakeDamage += TakeDamage;
        Player.OnGameOver += DisableHealthbar;
    }

    private void OnDestroy()
    {
        // Unsubscribe from player damage event
        Player.OnPlayerTakeDamage -= TakeDamage;
        Player.OnGameOver -= DisableHealthbar;
    }

    void Update() {
    
        // Health regeneration over time until full
        if(_health.minValue < _health.value
        && _health.value < _health.maxValue) {
            _health.value += 2f * Time.deltaTime;
        }

        
    }

    void TakeDamage(float damageValue) {
        _health.value -= damageValue;
    }

    void DisableHealthbar() {
        gameObject.SetActive(false);
    }
    
    
}