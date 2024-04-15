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
        
        float distSpawned = Vector3.Distance(Player.Instance.transform.position, worldSpawnPosition);
        
        if (distSpawned <= minDistance)
        {
            Debug.Log("Spawned too close to player, retrying...");
        }
        

        
        Asteroid asteroid =
            Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        
        // Set size of asteroid to be 5 10% of the time, 4 20% of the time, 3 40% of the time, 2 40% of the time
        int size = Random.Range(1, 11);
        if (size == 1)
        {
            asteroid.updateSize(5);
        }
        else if (size <= 3)
        {
            asteroid.updateSize(4);
        }
        else if (size <= 7)
        {
            asteroid.updateSize(3);
        }

        
        
    }
}