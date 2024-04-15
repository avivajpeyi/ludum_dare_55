using UnityEngine;
using Cinemachine;


public class CameraShake : StaticInstance<CameraShake>
{
    [SerializeField] private float shakeForce = 1f;
    
    public void ShakeCamera(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }
}