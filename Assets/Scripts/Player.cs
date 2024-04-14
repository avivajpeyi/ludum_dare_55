using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    // Enforce that the player is facing in front (along diurection of motion all the time)

    private Rigidbody2D _rb;
    public float maxSpeed = 30f;
    public float MaxHealth = 100f;
    private float _currentHealth = 100f;


    public static event Action<float> OnPlayerTakeDamage;

    public float speed
    {
        get { return _rb.velocity.magnitude; }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _currentHealth = MaxHealth;
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


    public void TakeDamage(float damage)
    {
        // Trigger the takedamage event
        MaxHealth -= damage;
        OnPlayerTakeDamage?.Invoke(damage);
    }

    public void Die()
    {
        // Destroy the player object
        Debug.Log("Player died!");
        Destroy(gameObject);
    }
}