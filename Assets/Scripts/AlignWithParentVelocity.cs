using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithParentVelocity : MonoBehaviour
{   
    // Align the front of the sprite with the direction of motion
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _dir;

    private Quaternion _offset;
    private void Start()
    {
        _rb = GetComponentInParent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        // current alignement
        _offset = transform.rotation;
    }
    
    private void Update()
    {
        _dir = _rb.velocity.normalized;
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg; 
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward) * _offset;
    }
}
