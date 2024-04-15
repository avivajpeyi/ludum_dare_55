using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DisplayOnStart : MonoBehaviour
{
    Vector3 initlocalScale;
    private void Start()
    {
        initlocalScale = transform.localScale;
        // DoTween animation to smoothly spawn the gameobject.
        transform.localScale = Vector3.zero;
    }


    private void Update()
    {
       if (Input.GetMouseButtonDown(0))
        {
            transform.DOScale(initlocalScale, 0.5f).SetEase(Ease.OutBounce);
        }
    }
}
