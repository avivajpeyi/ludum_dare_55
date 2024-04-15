using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private GoalSpawner spawner;
    private PlayerScore score;
    private CameraShake cameraShake;
    public GameObject explosionFX;
    private Player p;
    public AudioClip sound;

    private CinemachineImpulseSource impulseSource;
    void Start() {
        
        FindObjectOfType<GoalPointer>().currentGoal = this.gameObject;
        spawner = FindObjectOfType<GoalSpawner>();
        score = FindObjectOfType<PlayerScore>();
        cameraShake = FindObjectOfType<CameraShake>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        p = FindObjectOfType<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.playSound(sound, transform, 1f);
            Instantiate(explosionFX, transform.position, Quaternion.identity);
            score.IncreaseScore();
            spawner.SpawnGoal();
            
            cameraShake.ShakeCamera(impulseSource);
            //Add explosive force to all objects nearby
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 20f);
            foreach (var col in colliders)
            {
                Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 dir = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(dir * 20f, ForceMode2D.Impulse);
                }
            }

            p._currentHealth = p.MaxHealth;
            
            Destroy(this.gameObject, 0.1f);
        }
    }
}