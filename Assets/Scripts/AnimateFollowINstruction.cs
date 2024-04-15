using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnimateFollowINstruction : MonoBehaviour
{
    private TMP_Text _tmpText;

    private Tween _tween;
    // Start is called before the first frame update
    void Start()
    {
        _tmpText = GetComponent<TMP_Text>();
        // DoTween fade in and out (keep faded out for 3 seconds) and then fade in again and repeat and kill DoTween after 10 loops
        
        _tween = _tmpText.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo).SetDelay(10f);
        // kill the tween after 10 loops
        _tween.onStepComplete += () =>
        {
            if (_tween.CompletedLoops() == 20)
            {
                _tween.Kill();
                // Destroy the game object after the tween is killed
                Destroy(gameObject);
            }
        };
        
    }
}