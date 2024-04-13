using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyedParticles;

    public Rigidbody2D rb;
    public float size = 3f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.localScale = 5f * size * Vector3.one;
        rb.AddTorque(Random.Range(-0.01f, 0.01f), ForceMode2D.Impulse);
        RandomKick();

        // Register creation
        GameManager.Instance.asteroidCount++;
    }





    private void Update()
    {
        // If really far from Player, destroy
        if (Vector2.Distance(Player.Instance.transform.position, transform.position) >
            20f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDamage(10f * size);
            TakeDamage(0);
        }
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.asteroidCount--;
    }


    public void TakeDamage(float damage)
    {
        // If size > 1 spawn 2 smaller asteroids of size-1.
        if (size > 1)
        {
            for (int i = 0; i < 2; i++)
            {
                Asteroid newAsteroid = Instantiate(this, transform.position,
                    Quaternion.identity);
                newAsteroid.size = size - 1;
                
            }
        }


        // Spawn particles on destruction.
        if (destroyedParticles != null)
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);

        // Destroy this asteroid.
        Destroy(gameObject);
    }

    void RandomKick(float baseMagnitude = 3f)
    {
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        KickInDirection(direction, baseMagnitude);
    }

    void KickInDirection(Vector2 direction, float baseMagnitude = 3f)
    {
        float spawnSpeed = baseMagnitude + Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);
    }
}