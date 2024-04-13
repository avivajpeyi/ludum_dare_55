using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidSpawner : Singleton<AsteroidSpawner> {
    [SerializeField] private Asteroid asteroidPrefab;
  
    
    public int maxAsteroids = 10;
    public int asteroidCount = 0;

    private int level = 0;
    
    
    private void Update() {
        if (asteroidCount < maxAsteroids) {
            SpawnAsteroid();
        }
    }
    

    private void SpawnAsteroid() {
        // How far along the edge.
        float offset = Random.Range(0f, 1f);
        Vector2 viewportSpawnPosition = Vector2.zero;

        // Which edge.
        int edge = Random.Range(0, 4);
        if (edge == 0) {
            viewportSpawnPosition = new Vector2(offset, 0);
        } else if (edge == 1) {
            viewportSpawnPosition = new Vector2(offset, 1);
        } else if (edge == 2) {
            viewportSpawnPosition = new Vector2(0, offset);
        } else if (edge == 3) {
            viewportSpawnPosition = new Vector2(1, offset);
        }

        // Create the asteroid.
        Vector2 worldSpawnPosition = Camera.main.ViewportToWorldPoint(viewportSpawnPosition);
        Asteroid asteroid = Instantiate(asteroidPrefab, worldSpawnPosition, Quaternion.identity);
    
    }


}