using System.Collections.Generic; // Allows us to use Lists and Dictionaries 
using UnityEngine;// Connects to unity
using UnityEngine.UI;// Allows us to use and modify the UI Canvas Elements in unity (not TMPro)
using System; // Lets us use Serializable so that we can see the Struct in the inspector

public class Keybinds : MonoBehaviour
{
    /// <summary>
    /// A structure type (or struct type) is a value type that can encapsulate data and related functionality. 
    /// Action Map Data - 
    /// </summary>
    [Serializable]// to allow it and its fields to be broken down into a byte[] array (binary data) and passed around easily. 
    public struct ActionMapData
    {
        public string actionName;
        public Text keycodeDisplay;
        public string defaultKey;
    }
    [SerializeField] ActionMapData[] actionMapData;
    [SerializeField] GameObject currentSelectedKey;

    public static Dictionary<string,KeyCode> keys = new Dictionary<string,KeyCode>();

    [SerializeField] private Color32 selectedKey = new Color32(239, 116, 36, 225);
    [SerializeField] private Color32 changedKey = new Color32(39,171,249, 225);

    private void Start()
    {
        for (int i = 0; i < actionMapData.Length; i++)
        {
            keys.Add(actionMapData[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode),actionMapData[i].defaultKey));
            actionMapData[i].keycodeDisplay.text = keys[actionMapData[i].actionName].ToString();
        }
    }

    public void ChangeKey(GameObject clickedKey)
    {
        currentSelectedKey = clickedKey;
        if (currentSelectedKey != null)
        {
            currentSelectedKey.GetComponent<Image>().color = selectedKey;
        }
    }
    private void OnGUI()
    {
        string newKeyCode = "";
        Event changeKeyEvent = Event.current;

        if(currentSelectedKey != null)
        {
            if(changeKeyEvent.isKey)
            {
                if (!keys.ContainsValue(changeKeyEvent.keyCode))
                {
                    newKeyCode = changeKeyEvent.keyCode.ToString();
                    keys[currentSelectedKey.name] = changeKeyEvent.keyCode;
                    currentSelectedKey.GetComponentInChildren<Text>().text = newKeyCode;
                    currentSelectedKey.GetComponent<Image>().color = changedKey;
                    currentSelectedKey = null;
                }    
                else
                {
                    currentSelectedKey.GetComponent<Image>().color = Color.white;
                    currentSelectedKey = null;
                }
            }
        }
    }
}
