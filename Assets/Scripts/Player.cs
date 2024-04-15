using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private Color lightRed = new Color(180f/255f, 81f/255f, 84f/255f);
    private Color darkRed = new Color(128f/255f, 48f/255f, 66f/255f);
    private Color white = new Color(255f/255f, 255f/255f, 255f/255f);


    private CinemachineImpulseSource impulseSource;
    
    private Sprite _sprite;

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
        _healthbarUI.SetBarColor(white);
        cameraShake = FindObjectOfType<CameraShake>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        
    }

    private Vector2 dir
    {
        get { return _rb.velocity.normalized; }
    }


    private void OnDrawGizmos()
    {
        
        // Draw a line in the direction of the player
        Gizmos.color = Color.green;
        if (_rb != null)
        {
            Gizmos.DrawLine(transform.position, transform.position + (Vector3) dir);
        }
        
        
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
        cameraShake.ShakeCamera(impulseSource);
        
        damageCooldown = Time.time;
        _currentHealth -= damage;
        _healthbarUI.SetBarValue(_currentHealth);
        
        if (_currentHealth <= 0f)
        {
            Die();
        }
        else if (_currentHealth < MaxHealth / 5) {
            _healthbarUI.SetBarColor(darkRed);
        }
        else if (_currentHealth < MaxHealth / 2)
        {
            _healthbarUI.SetBarColor(lightRed);
        } 
        else
        {
            _healthbarUI.SetBarColor(white);
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