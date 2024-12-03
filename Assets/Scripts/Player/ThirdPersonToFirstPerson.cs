using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonToFirstPerson : MonoBehaviour
{
    public enum CameraMode
    {
        FirstPerson,
        ThirdPerson
    }
    public static float sensitivity = 15;
    [SerializeField] Vector2 rotationClamp = new Vector2(-60, 60);
    public bool invert = false;

    public float transitionSpeed = 5;
    public bool isLerping;

    public CameraMode cameraMode = CameraMode.FirstPerson;
    public Camera playerCamera;
    public Transform player;
    public Transform firstPersonSnap;
    public Transform thirdPersonSnap;
    public Transform thirdPersonParent;

    float yRotation;
    float rotation;
    void Look()
    {
        if (isLerping)
        {
            return;
        }
        player.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
        yRotation += Input.GetAxis("Mouse Y") * sensitivity;
        yRotation = Mathf.Clamp(yRotation, rotationClamp.x, rotationClamp.y);

        if (invert)
        {
            rotation = yRotation;
        }
        else
        {
            rotation = -yRotation;
        }
        if (cameraMode == CameraMode.FirstPerson)
        {
            firstPersonSnap.localEulerAngles = new Vector3(rotation, 0, 0);
        }
        else if (cameraMode == CameraMode.ThirdPerson)
        {
            thirdPersonParent.localEulerAngles = new Vector3(rotation, 0, 0);
        }
    }
    private IEnumerator SwitchCamera()
    {
        isLerping = true;
        Transform targetSnap = cameraMode == CameraMode.FirstPerson?firstPersonSnap : thirdPersonSnap;
        while (Vector3.Distance(playerCamera.transform.position, targetSnap.position) > 0.01f ||Quaternion.Angle(playerCamera.transform.rotation, targetSnap.rotation) > 0.01f)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, targetSnap.position, Time.deltaTime * transitionSpeed);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, targetSnap.rotation, Time.deltaTime * transitionSpeed);
            yield return null;
        }
        playerCamera.transform.position = targetSnap.position;
        playerCamera.transform.rotation = targetSnap.rotation;

        isLerping = false;
    }
    private void Update()
    {
        Look();
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (cameraMode == CameraMode.FirstPerson)
            {
                cameraMode = CameraMode.ThirdPerson;
            }
            else
            {
                cameraMode = CameraMode.FirstPerson;
            }
            StartCoroutine(SwitchCamera());

        }
    }
}
