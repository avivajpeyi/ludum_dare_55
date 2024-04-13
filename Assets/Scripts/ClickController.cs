using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : Singleton<ClickController>
{
    public List<GameObject> summonables;

    public float maxSize = 3f; // Maximum size of the object
    public float sizeIncreaseSpeed = 1f; // Speed at which the object size increases
    public float spawnDistance = 5f; // Distance from camera to spawn the object

    private bool isMouseDown = false;
    private Summonable summonableObj;
    private int spwnIndex = 0;

    Vector3 mousePos
    {
        get
        {
            Vector3 pos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }


    void FixedUpdate()
    {
        // If the mouse button is held down, increase the size of the object
        if (isMouseDown)
        {
            summonableObj.Grow();
        }
    }


    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");
            isMouseDown = true;
            int idx = Random.Range(0, summonables.Count);
            Debug.Log("Spwaned: " + summonables[idx].name);
            summonableObj = Instantiate(summonables[idx], mousePos, Quaternion
                .identity).GetComponent<Summonable>();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Mouse Up");
            isMouseDown = false;
            summonableObj.Summon();
            summonableObj = null;
        }
        
        // // Check if the mouse button is held down
        // if (Input.GetMouseButtonDown()(0) && summonableObj == null)
        // {

        // }
        // else if (isMouseDown&& summonableObj != null) // If the mouse button was held down and is now released
        // {
        //     summonableObj.GetComponent<Attractor>().Summon();
        //     summonableObj = null;
        //     isMouseDown = false;
        // }
    }
}