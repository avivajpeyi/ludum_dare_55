using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public AudioClip sfx;

    private bool _exploded = false;

    void Start()
    {
        Explode();
    }

    private void OnDrawGizmos()
    {
        // Draw a red circle at the position of the bomb
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        if (_exploded)
            return;

        Debug.Log("Bomb Exploded!");
        _exploded = true;

        Vector2 explosionPos = transform.position;

        

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D _rb = hit.GetComponent<Rigidbody2D>();
            if (_rb != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                _rb.AddForce(direction * power, ForceMode2D.Impulse);
            }
        }
    }
}