using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookZoom : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public static float sensitivity = 15;
    public bool invert = false;

    [Header("Rotation Clamping")]
    [SerializeField] Vector2 rotationClamp = new Vector2(-60, 60);

    [Header("Camera Transition Settings")]
    public float transitionSpeed = 5;
    public bool isLerping;

    [Header("Zoom Settings")]
    [SerializeField] float minZoom = 0f; // Fully first-person
    [SerializeField] float maxZoom = 5f; // Fully third-person
    [SerializeField] float zoomSpeed = 2f;
    private float currentZoom = 0f;

    [Header("Camera Setup")]
    public Transform playerCamera;
    public Transform player;
    public Transform firstPersonSnap;
    public Transform thirdPersonSnap;
    public Transform thirdPersonParent;

    float tempRotation;
    float verticalRotation;

    void HandleMouseLook()
    {
        player.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        tempRotation += Input.GetAxis("Mouse Y") * sensitivity;
        tempRotation = Mathf.Clamp(tempRotation, rotationClamp.x, rotationClamp.y);

        verticalRotation = invert ? tempRotation : -tempRotation;

        firstPersonSnap.localEulerAngles = new Vector3(verticalRotation, 0, 0);
        thirdPersonParent.localEulerAngles = new Vector3(verticalRotation, 0, 0);
    }
    private void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        currentZoom = Mathf.Clamp(currentZoom - scrollInput * zoomSpeed, minZoom, maxZoom);

        Vector3 targetPosition = Vector3.Lerp(firstPersonSnap.position, thirdPersonSnap.position, currentZoom / maxZoom);
        playerCamera.position = Vector3.Lerp(playerCamera.position, targetPosition, Time.deltaTime * transitionSpeed);
        playerCamera.GetComponent<Camera>().fieldOfView = Mathf.Lerp(40, 80, currentZoom / maxZoom);

    }
    private void Update()
    {
        HandleMouseLook();
        HandleZoom();
    }
}