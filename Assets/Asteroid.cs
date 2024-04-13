using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private ParticleSystem destroyedParticles;
    public int size = 3;


    private void Start()
    {
        // Scale based on the size.
        transform.localScale = 5f * size * Vector3.one;

        // Add movement, bigger asteroids are slower.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2(Random.value, Random.value).normalized;
        float spawnSpeed = Random.Range(4f - size, 5f - size);
        rb.AddForce(direction * spawnSpeed, ForceMode2D.Impulse);

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
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.asteroidCount--;
    }
}