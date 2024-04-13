using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    //making vairables
    private float speed = 5;
    private float dmg;
    private float health;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        speed += UnityEngine.Random.Range(0, 5);
        dmg = UnityEngine.Random.Range(15, 30);
        health = dmg;

        //change size
        transform.localScale = new Vector2(transform.localScale.x + dmg / 10, transform.localScale.y + dmg / 10);

        //set travel direction
        body.velocity = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this);
        }
    }

}
