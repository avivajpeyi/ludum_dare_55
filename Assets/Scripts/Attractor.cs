using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attractor : Summonable
{
    public GameObject triggerFx;
    GravitationalBody gravitationalBody;

    private ParticleSystem fieldFX;
    private Rigidbody2D rb;

    [SerializeField]
    private float initSize = 0.5f;
    [SerializeField]
    private float initDistance = 1f;
    [SerializeField]
    private float distanceMultiplier = 100f;
    


    void Start()
    {
        maxSize = 10f;
        rb = GetComponent<Rigidbody2D>();
        _size = initSize;
        gravitationalBody = GetComponent<GravitationalBody>();
        gravitationalBody.enabled = false;
        fieldFX = GetComponentInChildren<ParticleSystem>();
        UpdateField();
    }


    void UpdateField()
    {
        var main = fieldFX.main;
        main.scalingMode = ParticleSystemScalingMode.Local;
        fieldFX.transform.localScale = new Vector3(_size*3, _size*3, 1);
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        // Instantiate(triggerFx, transform.position, Quaternion.identity);
        gravitationalBody.RemoveItemFromList(GetComponent<Rigidbody2D>());
        // Fade out the object
    }

    public override void Summon()
    {
        gravitationalBody.enabled = true;

        Destroy(this.gameObject, 2f);
    }

    public override void Grow()
    {
        // Increase size of the attractor
        

        if (_size > maxSize)
        {
            return;
        }

        if (this == null)
            return;
        
        _size += 0.1f;
        transform.localScale = new Vector3(_size, _size, 1);
        UpdateField();
        
        // Increase the gravitational pull of the attractor
        rb.mass = Mathf.Pow(_size + 20, 2)/4;
        gravitationalBody.maxDistance = initDistance * _size * distanceMultiplier;
    }
}