using System;
using UnityEngine;
using UnityEngine.UI;  // For UI Text if using UI Text instead of TextMeshPro
using TMPro;  // For TextMeshPro
using DG.Tweening;
using UnityEngine.PlayerLoop; // Import DOTween namespace

public class TitleAnimation : MonoBehaviour
{
    private TMP_Text titleText;  // Use TMP_Text for TextMeshPro, replace with Text if using UI Text

    public Color startColor = Color.white;
    public Color glowColor = Color.yellow;
    public float glowDuration = 2f;
    public float moveAmount = 30f;
    public float moveDuration = 2f;
    public float fadeDuration = 1f;  // Duration for the fade out effect

    void Start()
    {
        titleText = GetComponent<TMP_Text>();
        
        DOTween.Init();
        titleText.color = startColor;  // Ensure starting color is set

        // Animate color and movement
        titleText.DOColor(glowColor, glowDuration).SetLoops(-1, LoopType.Yoyo);
        titleText.transform.DOScale(1.1f, moveDuration).SetLoops(-1, LoopType.Yoyo);

        
    }

    private void Update()
    {
        // On click
        if (Input.GetMouseButtonDown(0))
        {
            FadeOutTitle();
        }
    }


    public void FadeOutTitle()
    {
        // Stop all current animations
        titleText.DOKill();

        // Fade out the title
        titleText.DOFade(0, fadeDuration).OnComplete(() =>
        {
            // Optionally, do something after fade completes, like disable the GameObject
            titleText.gameObject.SetActive(false);
            Destroy(this.gameObject);
        });
    }
}