using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script that explodes when instantiated, creates some sfx particle fx, and emits a explosion radius with some force 
// to push objects away from the center of the explosion

public class Bomb : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;

    public GameObject explosionEffect;
    public AudioClip sfx;

    bool _exploded = false;
    
    
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
        Debug.Log("Bomb Exploded!");
        Vector2 explosionPos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D _rb = hit.GetComponent<Rigidbody2D>();
            if (_rb != null)
            {
                Vector2 direction =
                    (hit.transform.position - transform.position).normalized;
                _rb.AddForce(direction * power, ForceMode2D.Impulse);
                
                // send TakeDamage event to any rb with TakeDamage method
                hit.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                
            }
            
           
        }

        if (explosionEffect != null)
            Instantiate(explosionEffect, transform.position, transform.rotation);
        if (sfx != null)
            AudioSource.PlayClipAtPoint(sfx, transform.position);
        Destroy(gameObject, 0.5f);
    }
}