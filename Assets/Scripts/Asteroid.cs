using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyedParticles;

    public Rigidbody2D rb;
    public float size = 3f;
    private Player p;

    public float maxDistanceFromPlayer = 500f;
    public GameObject myprefab;
    private Collider2D col;

    private void Start()
    {
        p = FindObjectOfType<Player>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        
        col.enabled = false;
        rb.simulated = false;
        // DoTween animation to smoothly spawn the asteroid.
        transform.localScale = Vector3.zero;
        updateSize(size);


        
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

        rb.AddTorque(Random.Range(-0.01f, 0.01f), ForceMode2D.Impulse);
        RandomKick();

        Invoke("EnableCol", 0.3f);
        
        // Register creation
        AsteroidSpawner.Instance.asteroidCount++;
    }


    public void updateSize(float s)
    {
        size = s;
        Vector3 endSize = 5f * size * Vector3.one;
        // Do tween to smoothly scale the asteroid.
        transform.DOScale(endSize, 0.5f).SetEase(Ease.OutBounce);
        
        // transform.DOScale(endSize, 0.5f).SetEase(Ease.OutBounce);
        // Aftrer tween transform.localScale = 5f * size * Vector3.one;
    }

    void EnableCol()
    {
        col.enabled = true;
        // enable rb
        rb.simulated = true;
    }


    float DistFromPlayer
    {
        get
        {
            return Vector2.Distance(p.transform.position,
                transform.position);
        }
    }


    private void Update()
    {
        // If really far from Player, destroy
        if (DistFromPlayer > maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject==p.gameObject)
        {
            Debug.Log(collision.collider.name + " (player) collided with " + col.name + 
            " (asteroid) " + this.size);
            TakeDamage(0);
            p.TakeDamage(10f * size);
        }
    }

    private void OnDestroy()
    {
        if (AsteroidSpawner.Instance != null)
            AsteroidSpawner.Instance.asteroidCount--;
    }


    public void TakeDamage(float damage)
    {
        if (myprefab != null)
        {
            Instantiate(myprefab, transform.position, Quaternion.identity);
        }

        // If size > 1 spawn 2 smaller asteroids of size-1.
        if (size > 1)
        {
            for (int i = 0; i < 2; i++)
            {
                Asteroid newAsteroid = Instantiate(this, transform.position,
                    Quaternion.identity);
                newAsteroid.size = size - 1;
                Instantiate(myprefab, transform.position, Quaternion.identity);
            }
        }


        // Spawn particles on destruction.
        if (destroyedParticles != null)
            //Instantiate(myprefab, transform.position, Quaternion.identity);
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);

        // Destroy this asteroid.
        Destroy(gameObject);
    }

    void RandomKick(float baseMagnitude = 3f)
    {
        // Random direction along the unit circle.
        Vector2 direction = Random.insideUnitCircle.normalized;
        KickInDirection(direction, baseMagnitude);
    }

    void KickInDirection(Vector2 direction, float baseMagnitude = 3f)
    {
        float spawnSpeed = baseMagnitude + Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }
}