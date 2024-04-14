using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GoalSpawner spawner;
    private PlayerScore score;

    void Start() {
        
        FindObjectOfType<GoalPointer>().currentGoal = this.gameObject;
        spawner = FindObjectOfType<GoalSpawner>();
        score = FindObjectOfType<PlayerScore>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            score.IncreaseScore();
            spawner.SpawnGoal();
            Destroy(this.gameObject);
        }
    }
}