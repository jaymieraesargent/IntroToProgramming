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

    public CameraMode cameraMode = CameraMode.FirstPerson;
    public Camera playerCamera;
    public GameObject player;
    public Transform firstPersonSnap;
    public Transform thirdPersonSnap;
    public GameObject thirdPersonParent;

    float yRotation;
    public float rotation;
    void Look()
    {
        player.transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
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
            playerCamera.transform.localEulerAngles = new Vector3(rotation, 0, 0);
        }
        else if (cameraMode == CameraMode.ThirdPerson)
        {
            thirdPersonParent.transform.localEulerAngles = new Vector3(rotation, 0, 0);
        }
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
        }
    }
}
