using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : StaticInstance<ClickController>
{
    public List<GameObject> summonables;


    Vector3 mousePos
    {
        get
        {
            Vector3 pos = Helpers.Camera.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }


    void SpawnOnMouseClick()
    {
        
        int randomIndex = Random.Range(0, summonables.Count);
        Instantiate(summonables[randomIndex], mousePos, Quaternion.identity);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            SpawnOnMouseClick();
        }
    }
}