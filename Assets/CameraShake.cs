using UnityEngine;
using Cinemachine;

public class CameraShake : StaticInstance<CameraShake>
{
    
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Function to trigger camera shake with intensity and duration parameters
    public void ShakeCamera(float intensity, float duration)
    {
        // Set the noise values to shake the camera
        noise.m_AmplitudeGain = intensity;
        noise.m_FrequencyGain = 10f; // Adjust as needed

        // Invoke a method to reset the camera shake after the duration
        Invoke("StopShaking", duration);
    }

    // Method to stop the camera shake
    private void StopShaking()
    {
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
    }
}

