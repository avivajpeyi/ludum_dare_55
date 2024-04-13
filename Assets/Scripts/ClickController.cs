using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : Singleton<ClickController>
{
    public List<GameObject> summonables;


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


    void SpawnOnMouseClick()
    {
        int idx = Random.Range(0, summonables.Count);
        Instantiate(summonables[idx], mousePos, Quaternion.identity);
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            SpawnOnMouseClick();
        }
    }
}