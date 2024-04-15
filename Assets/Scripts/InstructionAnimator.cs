using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InstructionAnimator : MonoBehaviour
{
    private void Update()
    {
        // On click
        if (Input.GetMouseButtonDown(0))
        {
            FadeOutTween();
        }
    }

    void FadeOutTween()
    {
        // Use DOTween to scale out the instruction text
        transform.DOScale(0, 1f).OnComplete(() =>
        {
            // Optionally, do something after fade completes, like disable the GameObject
            gameObject.SetActive(false);
            Destroy(this.gameObject);
        });
    }
}