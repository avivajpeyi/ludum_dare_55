using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AnimateFollowINstruction : MonoBehaviour
{
    private TMP_Text _tmpText;

    // Start is called before the first frame update
    void Start()
    {
        _tmpText = GetComponent<TMP_Text>();
        // DoTween fade in and out (keep faded out for 3 seconds) and then fade in again and repeat
        _tmpText.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo).SetDelay(10f);


        Destroy(this, 60);
    }
}