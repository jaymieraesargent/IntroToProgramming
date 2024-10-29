using System.Collections.Generic; // System.Collections.Generic: Allows access to generic collections like List and Dictionary, useful for managing large or varied datasets.
using UnityEngine;// UnityEngine: Provides access to core Unity features (such as MonoBehaviour) for game object scripting.
using UnityEngine.UI;// UnityEngine.UI: Allows us to manipulate UI elements (e.g., Text and Image) for updating the canvas.(not TMPro)
using System; //System: Includes basic 
//Keybinds : MonoBehaviour: This class inherits from MonoBehaviour, making it a Unity script that can be attached to GameObjects.
public class Keybinds : MonoBehaviour
{ /*
    The [Serializable] attribute in C# is used to indicate that a class or struct can be "serialized." 
    Serialization is the process of converting an object’s state (its data) into a format that can be stored (such as in memory or on disk) or sent across a network. 
    Unity Inspector Display:

    When a struct or class is marked [Serializable] in Unity, Unity can display it in the Inspector window. 
    This allows developers to edit and view the fields of the struct directly in the Unity Editor, making it easier to set initial values and tweak data without writing additional code.
    For example, in this case, ActionMapData is marked as [Serializable], so all fields (like actionName, keycodeDisplay, and defaultKey) will be visible in the Inspector for easy editing.

    Data Conversion (Serialization):

    Beyond Unity, [Serializable] allows the struct to be broken down into a series of bytes, known as a byte[] array (or binary data).  
    This is useful when you need to save the struct’s data to a file, send it over a network, or store it for later use.
    Converting to a byte[] array can make data storage and transfer more efficient, as it’s in a compact binary format.

    In summary, [Serializable] makes it easy to work with data in the Unity Editor by making fields visible in the Inspector and enables data storage, sharing, and transfer by allowing the struct to be converted into binary format.
  */
    [Serializable]
    // ActionMapData Struct: This structure holds information for each action that can have a keybinding:
    public struct ActionMapData
    {
        public string actionName; // Name of the action(e.g., "Jump").
        public Text keycodeDisplay; // UI Text element showing the current keybinding.
        public string defaultKey; // The default key set for the action.
    }
    [SerializeField] ActionMapData[] actionMapData; // Array of ActionMapData structs to store keybinding data for each action.
    [SerializeField] GameObject currentSelectedKey; // Tracks the key currently being modified, allowing us to visually indicate the selected key in the UI.
    // By storing keybindings in a dictionary, we can easily look up the assigned key for any action during gameplay. This setup also makes it easy to update or change bindings later, as each action has a unique key in the dictionary.
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>(); // Stores action names (e.g., "Jump") mapped to KeyCode values (e.g., KeyCode.Space), making it easy to access keybindings during gameplay.
    [SerializeField] private Color32 selectedKey = new Color32(239, 116, 36, 225);    // Colors for visual feedback when a keybinding is being selected.
    [SerializeField] private Color32 changedKey = new Color32(39, 171, 249, 225);    // Colors for visual feedback when a keybinding is being changed.
    /// <summary>
    /// The Start method in Unity is called once at the beginning, right after the object is created in the scene. In this method, we initialize the keybindings by looping through each entry in actionMapData, setting up both the key mapping and the UI display.
    /// </summary>
    private void Start()
    {
        // Loops through through each item in the actionMapData array, which holds information about each action (like "Jump" or "MoveForward") and its associated default key.
        // Looping through the actionMapData array ensures that each action gets an initial keybinding set up based on the default values, so we don’t have to manually initialize each one.
        for (int i = 0; i < actionMapData.Length; i++)
        {
            // Add the Action and KeyCode to the keys Dictionary: This line adds the action name (like "Jump") and its corresponding KeyCode to the keys dictionary.
            // Convert defaultKey to a KeyCode Using Enum.Parse: Enum.Parse takes a string (like "Space") and converts it into the corresponding KeyCode (like KeyCode.Space), which Unity uses for detecting specific key inputs.
            // Enum.Parse enables us to store key names as text strings in defaultKey and convert them to KeyCode values programmatically, making the setup more flexible and readable.
            keys.Add(actionMapData[i].actionName, (KeyCode)Enum.Parse(typeof(KeyCode), actionMapData[i].defaultKey));
            // Update the UI Text to Show the Assigned Key: This line sets the text property of the keycodeDisplay UI element to show the KeyCode assigned to the action.
            // Updating the keycodeDisplay ensures that the player sees the current keybinding in the UI. Using ToString() on the KeyCode displays it in a readable format (like "Space" or "W"), making it clear to the player which key is assigned to each action.
            actionMapData[i].keycodeDisplay.text = keys[actionMapData[i].actionName].ToString();
        }
        /* 
        Summary of the above
            We loop through each action in actionMapData.
            Convert each defaultKey string to a KeyCode.
            Store the action and KeyCode in the keys dictionary.
            Update the UI to reflect the current keybinding for each action.
         */
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

        if (currentSelectedKey != null)
        {
            if (changeKeyEvent.isKey)
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
