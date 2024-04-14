using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GoalSpawner spawner;

    void Start() {
        FindObjectOfType<GoalPointer>().currentGoal = this.gameObject;
        spawner = FindObjectOfType<GoalSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Triggered");
        if (other.CompareTag("Player"))
        {
            spawner.SpawnGoal();
            Destroy(this.gameObject);
        }
    }
}