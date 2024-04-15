using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletTIme : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 3f;

    void Update ()
    {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void DoSlowmotion ()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    
    public void StopSlowmotion()
    {
        // DoTween to smoothly tween the time scale back to normal
        DOTween.To(() => Time.timeScale, // Getter
            scale => Time.timeScale = scale, // Setter
            1, // Target value
            1); // Duration of the tween
    }

}
