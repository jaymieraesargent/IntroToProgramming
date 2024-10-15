using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Keybinds : MonoBehaviour
{
    [Serializable]
    public struct ActionMapData
    {
        public string actionName;
        public Text keycodeDisplay;
        public string defaultKey;
    }
    [SerializeField] ActionMapData[] actionMapData;
    public GameObject currentSelectedKey;

    public void ChangeKey(GameObject clickedKey)
    {

    }
}
