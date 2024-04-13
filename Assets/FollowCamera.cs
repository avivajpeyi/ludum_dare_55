using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    public Transform
        player; // Reference to the player object// Reference to the Cinemachine Virtual Camera

    public float minFOV = 40f; // Minimum field of view
    public float maxFOV = 60f; // Maximum field of view
    public float zoomSpeed = 5f; // Speed of zooming

    private CinemachineFramingTransposer
        transposer; // Reference to Cinemachine Framing Transposer


    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        player = Player.Instance.transform;
        virtualCamera.Follow = player;
        if (virtualCamera != null)
        {
            transposer = virtualCamera
                .GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }


    void Update()
    {
        if (player == null || transposer == null)
            return;

        // Calculate the zoom based on player speed
        float speed = Player.Instance.speed;
        float targetFOV =
            Mathf.Lerp(maxFOV, minFOV,
                speed / 10f); // Adjust divisor to control zoom sensitivity

        // Smoothly adjust the field of view
        transposer.m_CameraDistance = Mathf.Lerp(transposer.m_CameraDistance, targetFOV,
            Time.deltaTime * zoomSpeed);
    }
}