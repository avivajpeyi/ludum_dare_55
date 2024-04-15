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
    private float minDistance = 100f;
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
        Vector2 worldSpawnPosition = Player.Instance.transform.position +
                                     Random.insideUnitSphere.normalized *
                                     Random.Range(minDistance, maxDistance);
        
        
        Vector2 dir = (worldSpawnPosition - (Vector2) Player.Instance.transform.position).normalized;
        worldSpawnPosition += (dir * 30f);
        
        float distSpawned = Vector3.Distance(Player.Instance.transform.position, worldSpawnPosition);
        
        if (distSpawned <= minDistance)
        {
            Debug.Log("Spawned too close to player, SKIPPING...");
            return;
            
        }
        Debug.Log("dists " + distSpawned);
        

        
        Asteroid asteroid =
            Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        
        // Set size of asteroid to be 5 5% of the time, 4 5% of the time, 3 40% of the time, 2 50% of the time
        int size = Random.Range(1, 101);
        if (size <= 5)
        {
            asteroid.updateSize(5);
        }
        else if (size <= 10)
        {
            asteroid.updateSize(4);
        }
        else if (size <= 50)
        {
            asteroid.updateSize(3);
        }
        else
        {
            asteroid.updateSize(2);
        }
        
        
    }
}