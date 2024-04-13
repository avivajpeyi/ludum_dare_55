using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Spawns items in the scnene, nearby the player. deletes objects that are far away from the player
public class AsteroidSpawner : MonoBehaviour
{
    Player player;

    public GameObject asteroidPrefab;


    public float spawnRadius = 10f;

    public float despawnRadius = 20f;

    // max count nearby 
    public int maxCount = 10;

    List<GameObject> asteroids = new List<GameObject>();

    // current count
    int count
    {
        get { return asteroids.Count; }
    }


    void Start()
    {
        player = Player.Instance;
    }

    void Update()
    {
        if (player == null)
            return;

        if (count < maxCount)
        {
            SpawnAsteroid();
        }

        DespawnAsteroids();
    }

    void SpawnAsteroid()
    {
        Vector2 randomPos = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPos = player.transform.position +
                           new Vector3(randomPos.x, randomPos.y, 0);
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        asteroids.Add(asteroid);
    }

    void DespawnAsteroids()
    {
        foreach (GameObject asteroid in asteroids)
        {
            if (Vector2.Distance(asteroid.transform.position, player.transform.position) >
                despawnRadius)
            {
                // Destroy the object and remove it from the list
                asteroids.Remove(asteroid);
                Destroy(asteroid);
            }
        }
    }
}