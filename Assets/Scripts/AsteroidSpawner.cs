using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

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


    private void SpawnAsteroid()
    {
        // How far along the edge.
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        // Which edge.
        int edge = Random.Range(0, 4);
        if (edge == 0)
        {
            viewportSpawnPosition = new Vector2(offset, 0);
        }
        else if (edge == 1)
        {
            viewportSpawnPosition = new Vector2(offset, 1);
        }
        else if (edge == 2)
        {
            viewportSpawnPosition = new Vector2(0, offset);
        }
        else if (edge == 3)
        {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        // Move a bit away from the edge.
        viewportSpawnPosition += viewportSpawnPosition.normalized * 0.4f;
        
        
        // Create the asteroid.
        Vector2 worldSpawnPosition = Helpers.Camera.ViewportToWorldPoint
        (viewportSpawnPosition
        );
        Asteroid asteroid =
            Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
        
        
    }
}