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
            Store the action and KeyCode (which we convert each defaultKey string to a KeyCode) in the keys dictionary.
            Update the UI to reflect the current keybinding for each action.
         */
    }
    /// <summary>
    /// Called when the user selects a keybinding to modify. This function is connected to a button that is also named the same name as an action.
    /// </summary>
    /// <param name="clickedKey"> This object is the button that the event is attached to, the button object must be named the same as the action you are tying to change</param>
    public void ChangeKey(GameObject clickedKey)
    {
        // Sets currentSelectedKey to the UI element the user clicked on.
        currentSelectedKey = clickedKey;
        /*
            If currentSelectedKey is null, it means the player hasn’t clicked on any keybinding UI element, so no change is needed.
            If currentSelectedKey is not null, it means a UI element has been clicked, indicating that the player wants to reassign a key for that specific action.
            This check prevents errors that could occur if the code tries to modify or access properties of currentSelectedKey when it hasn’t been set. 
            It’s a safety check to ensure the program only proceeds if there is actually a selected key to update.
         */
        // Check to see if the variable currentSelectedKey has been assigned a value or not, only run the code below if it has been assigned.
        if (currentSelectedKey != null)
        {
            // Changes its color to selectedKey, visually showing it’s ready for reassignment.
            currentSelectedKey.GetComponent<Image>().color = selectedKey;
        }
    }
    /*
        OnGUI is called several times per frame in Unity, especially during events like clicks, key presses, or layout updates.
        Unity processes different GUI events (e.g., mouse clicks, key presses) and can detect when these events occur.
        OnGUI method because handles various events (such as mouse clicks, key presses, window resizing, and layout changes) multiple times per frame.
     */
    private void OnGUI()
    {
        /*
            The Event class in Unity captures details of the current event happening in OnGUI. 
            By checking properties like Event.current, we can see if a key was pressed or if another event occurred and act accordingly.
         */
        //  Represents the current input event (e.g., key press).
        Event changeKeyEvent = Event.current;
        /*
             This conditional checks if a keybinding UI element is selected. 
             If currentSelectedKey is null, it means no keybinding change is active, and we don’t need to process any inputs.
         */
        // Ensures there’s a key ready to be reassigned.
        if (currentSelectedKey != null)
        {
            /*
                changeKeyEvent.isKey: isKey is a property of Unity's Event class. 
                It returns true if the current event (changeKeyEvent) is a keyboard event (i.e., if a key was pressed).
                This check ensures that the code inside the if statement only runs if a key press is detected. 
                Without this check, the code could mistakenly respond to events that aren't key presses.
            */
            // Checks if the input event is a key press.
            if (changeKeyEvent.isKey)
            {
                /*
                    This line checks if changeKeyEvent.keyCode (the new key the player pressed) is already assigned to any other action in the keys dictionary.
                    keys.ContainsValue(changeKeyEvent.keyCode) returns true if any action in keys already has the new key.
                    Adding ! (not) before it makes the expression true only if the key is not already in use.
                */
                // Ensures the new key isn’t already assigned (Already Used).
                if (!keys.ContainsValue(changeKeyEvent.keyCode))
                {
                    /* 
                        This line assigns the new key to the selected action. 
                        currentSelectedKey.name is the key in the dictionary, representing the name of the action (e.g., "Jump"), and changeKeyEvent.keyCode is the new KeyCode the player pressed.
                        This allows the new key to replace the old one in the dictionary only if it’s unique, preventing conflicts with other actions.
                    */
                    // Updates the dictionary with the new KeyCode.
                    keys[currentSelectedKey.name] = changeKeyEvent.keyCode;
                    /*
                        This line updates the text component of the currentSelectedKey to display the new key. 
                        ToString() converts the KeyCode to a readable format (e.g., KeyCode.Space to "Space").
                        This visually informs the player of the updated keybinding on the UI, showing which key is now bound to the selected action.
                     */
                    // Changes the UI text to display the new key.
                    currentSelectedKey.GetComponentInChildren<Text>().text = changeKeyEvent.keyCode.ToString();
                    /*
                        Changes the currentSelectedKey's UI element color to changedKey, visually confirming the change for the user.
                        The color change acts as feedback for the player, indicating that the binding update was successful.                        
                     */
                    // Changes the color to indicate the keybinding has been successfully updated.
                    currentSelectedKey.GetComponent<Image>().color = changedKey;
                    /*
                        Setting currentSelectedKey to null after that ensures that no keybinding is currently selected, resetting the system for the next input.
                        Setting currentSelectedKey to null also prevents unintended updates by clearing the selection.
                     */
                    //  Sets currentSelectedKey to null to finish the reassignment.
                    currentSelectedKey = null;
                }
                // Handle Cases When the Key Is Already in Use
                else
                {
                    // Reverts the color of currentSelectedKey to Color.white to indicate that the new key wasn’t accepted.
                    currentSelectedKey.GetComponent<Image>().color = Color.white;
                    // Clears the selection so the user can try again.
                    currentSelectedKey = null;
                }
            }
        }
    }
}
