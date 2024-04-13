using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attractor : MonoBehaviour
{

    public GameObject triggerFx;
    GravitationalBody gravitationalBody;
    
    void Start()
    {
        gravitationalBody = GetComponent<GravitationalBody>();
        Destroy(this.gameObject, 2f);
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
    
    
}
