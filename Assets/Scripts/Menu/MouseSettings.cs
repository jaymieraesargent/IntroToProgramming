using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSettings : MonoBehaviour
{
    public void ChangeSensitivity(float strength)
    {
       MouseLook.sensitivity = strength;
    }
    public void ChangeInvert(bool isInverted)
    {
        MouseLook.invert = isInverted;
    }
}
