using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    // Enforce that the player is facing in front (along diurection of motion all the time)
    
    private Rigidbody2D _rb;
    public float maxSpeed = 30f;
    
    
    public float speed
    {
        get
        {
            return _rb.velocity.magnitude;
        }
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private Vector2 dir
    {
        get
        {
            return _rb.velocity.normalized;
        }
    }
    
    private void Update()
    {
        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        // Clamp the speed of the player
        _rb.velocity = Vector2.ClampMagnitude(_rb.velocity, maxSpeed);
        
    }
    
    
}
