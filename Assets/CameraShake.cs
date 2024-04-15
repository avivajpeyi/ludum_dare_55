using UnityEngine;
using Cinemachine;


public class CameraShake : StaticInstance<CameraShake>
{
    [SerializeField] private float shakeForce = 1f;


    // Function to trigger camera shake with intensity and duration parameters
    public void ShakeCamera(CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(shakeForce);
    }

    // M
}