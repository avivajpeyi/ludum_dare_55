using UnityEngine;
using UnityEngine.UI;  // For UI Text if using UI Text instead of TextMeshPro
using TMPro;  // For TextMeshPro
using DG.Tweening;  // Import DOTween namespace

public class TitleAnimation : MonoBehaviour
{
    public TMP_Text titleText;  // Use TMP_Text for TextMeshPro, replace with Text if using UI Text

    public Color startColor = Color.white;
    public Color glowColor = Color.yellow;
    public float glowDuration = 2f;
    public float moveAmount = 30f;
    public float moveDuration = 2f;
    public float fadeDuration = 1f;  // Duration for the fade out effect

    void Start()
    {
        DOTween.Init();
        titleText.color = startColor;  // Ensure starting color is set

        // Animate color and movement
        titleText.DOColor(glowColor, glowDuration).SetLoops(-1, LoopType.Yoyo);
        titleText.transform.DOMoveY(titleText.transform.position.y + moveAmount, moveDuration)
            .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);

        // Optionally, attach click event listener if not using a button
        if (titleText.GetComponent<Button>() == null)
        {
            titleText.gameObject.AddComponent<Button>(); // Add a Button component if there isn't one
        }
        titleText.GetComponent<Button>().onClick.AddListener(FadeOutTitle);
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
        });
    }
}
