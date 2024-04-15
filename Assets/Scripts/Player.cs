using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    // Enforce that the player is facing in front (along direction of motion all the time)


    private Rigidbody2D _rb;
    public float maxSpeed = 20f;
    public float MaxHealth = 100f;
    public float _currentHealth;
    private PlayerHealthbarUI _healthbarUI;

    public AudioClip getHit;
    public AudioClip destroyed;

    private Color lightRed = new Color(180f / 255f, 81f / 255f, 84f / 255f);
    private Color darkRed = new Color(128f / 255f, 48f / 255f, 66f / 255f);
    private Color white = new Color(255f / 255f, 255f / 255f, 255f / 255f);

    Tween healthImageTween;
    public Image playerHealthFlashImage;


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
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)dir);
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

    public void Heal()
    {
        _currentHealth = MaxHealth;
        // DoTween the healthbar to the max health
        // set the healthbar value to white
        _healthbarUI.SetBarColor(white);
        _healthbarUI.SetBarValue(_currentHealth);
        DOTween.To(() => _currentHealth, x => _currentHealth = x, MaxHealth, 0.5f)
            .OnUpdate(() => { _healthbarUI.SetBarValue(_currentHealth); })
            .SetEase(Ease.Linear);
    }
    


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
            SoundManager.instance.playSound(destroyed, transform, 1f);
        }
        else
        {
            SoundManager.instance.playSound(getHit, transform, 1f);
        }

        if (_currentHealth <= 0f)
        {
            Die();
            FlashHpImage(false);
        }
        else if (_currentHealth < MaxHealth / 5)
        {
            _healthbarUI.SetBarColor(darkRed);
            FlashHpImage(true);
        }
        else if (_currentHealth < MaxHealth / 2)
        {
            _healthbarUI.SetBarColor(lightRed);
            FlashHpImage(false);
        }
        else
        {
            _healthbarUI.SetBarColor(white);
            FlashHpImage(false);
        }

        // ScreenShake
    }


    public void FlashHpImage(bool flash)
    {
        healthImageTween = playerHealthFlashImage.DOFade(0.2f, 1f).SetLoops(2,
            LoopType.Yoyo);

        if (flash)
        {
            healthImageTween.Restart();
        }
        else
        {
            healthImageTween.Pause();
            playerHealthFlashImage.DOFade(0f, 0.05f);
        }
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