using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : MonoBehaviour
{
    //making vairables
    public float speed;
    private float xDir;
    private float yDir;
    public Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 scaleUp = new Vector2(transform.localScale.x + UnityEngine.Random.value, transform.localScale.y + UnityEngine.Random.value);
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
}
