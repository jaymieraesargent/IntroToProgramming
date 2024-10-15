using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class MouseLook : MonoBehaviour
    {
        //Create a public enum call RotationalAxis
        public enum RotationalAxis
        {
            //MouseX, MouseY
            MouseX,
            MouseY
        }
        //Create a link or reference to rotational axis call axis and set the defualt axis X
        [SerializeField] RotationalAxis axis = RotationalAxis.MouseX;
        //Sensitivity of moving the mouse around the screen (how fast the camera rotates)
        public static float sensitivity = 15;
        //Minimum and Maximum values the head can look up and down
        [SerializeField] Vector2 rotationClamp = new Vector2(-60, 60);
        //Temp value to help us calculate the up and down rotation
        float yRotation;
        //Value to control the invert swap for up and down camera control
        public static bool invert = false;
        void Start()
        {            

            //If our gameobject has a rigidbody attached to it
            if (GetComponent<Rigidbody>())
            {
                //Then freeze the rigidbody constraints
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            //If the game object is a camera 
            if (GetComponent<Camera>())
            {
                //Set our axis to Y
                axis = RotationalAxis.MouseY;
            }
        }
        void Look()
        {
            #region Mouse Movement X axis
            //If the rotational axis is mouse x
            if (axis == RotationalAxis.MouseX)
            {
                //Transform the rotation on our gameobjects Y by our mouse X times sensitivity
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }
            #endregion
            #region Mouse Movement Y axis
            //Else we are on the rotation axis of mouse Y
            else
            {
                //Our rotational Y plus equals our mouse input for mouse Y times sensitivity
                yRotation += Input.GetAxis("Mouse Y") * sensitivity;
                //Clamp the rotation y axis using Mathf and between our min and max
                yRotation = Mathf.Clamp(yRotation, rotationClamp.x, rotationClamp.y);
                //If inverted mouse look (down is up)
                if (invert)
                {
                    //transform our local rotation angles to the new rotation
                    transform.localEulerAngles = new Vector3(yRotation, 0, 0);
                }
                //else if (up is up)
                else
                {
                    //transform our local rotation angles to the new - rotation
                    transform.localEulerAngles = new Vector3(-yRotation, 0, 0);
                }
            }
            #endregion

        }
        private void Update()
        {
            if (GameManager.instance.currentGameState == GameState.Game)
            {
                Look();
            }
        }
    }
}