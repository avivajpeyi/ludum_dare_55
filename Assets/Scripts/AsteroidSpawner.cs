using System;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AsteroidSpawner : Singleton<AsteroidSpawner>
{
    [SerializeField] private Asteroid asteroidPrefab;

    
    [SerializeField]
    private int maxAsteroids = 30;
    [SerializeField]
    private  int numToSpawn = 5;
    [SerializeField]
    public int asteroidCount = 0;
    [SerializeField]
    private float timeBwSpawns = 2f;
    private float timeSinceLastSpawn = 0f;

    [SerializeField]
    private float minDistance = 30f;
    [SerializeField]
    private float maxDistance = 400f;

    void Update()
    {
        if (asteroidCount < maxAsteroids)
        {
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn > timeBwSpawns)
            {
                for (int i = 0; i < numToSpawn; i++)
                    SpawnAsteroid();
                timeSinceLastSpawn = 0f;
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a sphere around min and max distance
        Gizmos.color = Color.magenta;
        Vector3 p = Vector3.zero;
        if (Player.Instance != null)
        {
            p = Player.Instance.transform.position;
        }
        
        Gizmos.DrawWireSphere(p, minDistance);
        Gizmos.DrawWireSphere(p, maxDistance);
        
    }


    private void SpawnAsteroid()
    {
        // Get random position in world around the player
        Vector3 worldSpawnPosition = Player.Instance.transform.position +
                                     Random.insideUnitSphere.normalized *
                                     Random.Range(minDistance, maxDistance);
        
        
        Asteroid asteroid =
            Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        
        // Set size of asteroid to be 3 or 2 (20% of time)
        asteroid.size = Random.value < 0.2f ? 3 : 2;
        
        
    }
}