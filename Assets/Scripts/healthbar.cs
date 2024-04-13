using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthbarUI : Singleton<PlayerHealthbarUI> {
    private Slider _health;
    private KeyCode _damageKey = KeyCode.Space;

    void Start() {
        _health = GetComponent<Slider>();
        _health.minValue = 0f;
        _health.maxValue = 100f;
        _health.value = 100f; // Health bar starts full
        
        // Subscribe to player damage event
        Player.OnPlayerTakeDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        // Unsubscribe from player damage event
        Player.OnPlayerTakeDamage -= TakeDamage;
    }

    void Update() {
    
        // Health regeneration over time until full
        if(_health.minValue < _health.value
        && _health.value < _health.maxValue) {
            _health.value += 5f * Time.deltaTime;
        }
        
        if(Input.GetKeyDown(_damageKey)) { //TODO: Replace key press with event trigger
            TakeDamage(20f); //TODO: Replace hard coded damage value with value recieved from collision event
        }

        if(_health.value == _health.minValue)
        {
            // Player.Instance.Die();
            Debug.Log("Player died!");
        }
    }

    void TakeDamage(float damageValue) {
        _health.value -= damageValue;
    }

}