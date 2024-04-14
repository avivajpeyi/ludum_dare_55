using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{

    void Start() {
        Debug.Log("Goal is being set.");
        // GoalPointer.Instance.currentGoal = this.gameObject;
        FindObjectOfType<GoalPointer>().currentGoal = this.gameObject;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collider Triggered");
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    void OnDestroy()
    {
        Debug.Log("On Destroy Triggered");
        // Instantiate(triggerFx, transform.position, Quaternion.identity);
        //gravitationalBody.RemoveItemFromList(GetComponent<Rigidbody2D>());
        // Fade out the object
    }
}