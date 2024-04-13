using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    private Player p;
    public float orthoBaseSize = 10;
    public float orthoSizeChangeDuration = 0.5f;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        p = Player.Instance;
        virtualCamera.Follow = p.transform;
    }

    void UpdateOrthographicSize()
    {
        float newOrthoSize = orthoBaseSize + p.speed / 2;
        
        // Use DOTween to smoothly tween the orthographic size
        DOTween.To(() => virtualCamera.m_Lens.OrthographicSize, // Getter
            size => virtualCamera.m_Lens.OrthographicSize = size, // Setter
            newOrthoSize, // Target value
            orthoSizeChangeDuration); // Duration of the tween
    }

    void Update()
    {
        UpdateOrthographicSize();
    }
}