using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Summonable
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public AudioClip sfx;
    public GameObject explosionEffect;

    

    void Start()
    {
        maxSize = 1f;
        _size = 1f;
        Explode();
        SoundManager.instance.playSound(sfx, transform, 1f);
        Destroy(this, 0.1f);
    }

    private void OnDrawGizmos()
    {
        // Draw a red circle at the position of the bomb
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        Vector2 explosionPos = transform.position;
        // Instantiate the explosion effect
        Instantiate(explosionEffect, explosionPos, Quaternion.identity);
        

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPos, radius);
        foreach (Collider2D hit in colliders)
        {
            Rigidbody2D _rb = hit.GetComponent<Rigidbody2D>();
            if (_rb != null)
            {
                Vector2 direction =
                    (hit.transform.position - transform.position).normalized;
                _rb.AddForce(direction * power, ForceMode2D.Impulse);
                
                if (hit!=null)
                    hit.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
            }
        }
    }
    
    public override void Summon()
    {   
        Explode();
        Destroy(this, 0.1f);
    }
    
    public override void Grow()
    {
        // Nothing
    }

}