using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Player : Singleton<Player>
{
    // Enforce that the player is facing in front (along direction of motion all the time)

    public GameObject gameOverFX;
    private Rigidbody2D _rb;
    public float maxSpeed = 20f;
    public float MaxHealth = 100f;
    private float _currentHealth;
    private PlayerHealthbarUI _healthbarUI;

    private CinemachineImpulseSource impulseSource;

    public static event Action OnGameOver;
    public void TriggerGameOver() => OnGameOver?.Invoke();
    private CameraShake cameraShake;

    public float speed
    {
        get { return _rb.velocity.magnitude; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = MaxHealth;
        _healthbarUI = FindObjectOfType<PlayerHealthbarUI>();
        _healthbarUI.InitBar(_currentHealth);
        cameraShake = FindObjectOfType<CameraShake>();
    }

    private Vector2 dir
    {
        get { return _rb.velocity.normalized; }
    }


    private void FixedUpdate()
    {
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // Clamp the speed of the player
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
    }




    float damageCooldown = 0.5f;

    public void TakeDamage(float damage)
    {
        // if has just taken damange, skip
        if (Time.time - damageCooldown < 0.5f)
        {
            return;
        }
        CameraShake.Instance.ShakeCamera(impulseSource);
        
        damageCooldown = Time.time;
        _currentHealth -= damage;
        _healthbarUI.SetBarValue(_currentHealth);
        
        if (_currentHealth <= 0)
        {
            Die();
        }
        
        // ScreenShake
        
        
        
    }

    public void Die()
    {
        // Destroy the player object
        Debug.Log("Player died!");
        _healthbarUI.DisableHealthbar();
        TriggerGameOver();
        // Play the Game over FX
        _rb.velocity = Vector2.zero;
        // Make _rb fixed postion (cant move)
        _rb.isKinematic = true;
        
    }
}