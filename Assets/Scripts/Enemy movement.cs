using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    //making vairables
    private float speed = 5;
    private float xDir;
    private float yDir;
    private float dmg;
    private float health;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        speed += UnityEngine.Random.Range(0, 5);
        dmg = UnityEngine.Random.Range(15, 30);
        health = dmg;
        Vector2 scaleUp = new Vector2(transform.localScale.x + dmg/10, transform.localScale.y + dmg/10);
        transform.localScale = scaleUp;
        xDir = UnityEngine.Random.value;
        yDir = UnityEngine.Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(xDir, yDir).normalized;
        body.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
