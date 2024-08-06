
#region Region Comments and notes
//octothorpe = # = Hash
/*
    The #region and #endregion directives in C# are used to organize and group code into collapsible sections in your code editor.

    #region: Starts a collapsible section of code.
    #endregion: Ends the collapsible section.
    This helps keep your code tidy and makes it easier to navigate by allowing you to collapse or expand sections as needed. 
 */
#endregion
#region Using
/*
    The using UnityEngine; statement in your Unity script gives you access to all the basic tools
    and features of the Unity game engine. This includes creating and managing game objects,    
    handling player input, working with physics, rendering graphics, playing sounds,    
    and debugging your game. Essentially, it lets you use all the essential components 
    you need to build and control your game in Unity.
 */
#endregion
using UnityEngine;
#region NameSpace
/*
    A namespace in C# is like a container that helps organize your code and avoid name 
    conflicts. It groups related classes, functions, and other code elements together, 
    making your code easier to manage and read. For example, Unity uses the UnityEngine 
    namespace to group all the core game engine features in one place.
 */
#endregion
namespace Player
{
    #region RequireComponent
    /*
        The [RequireComponent(typeof(CharacterController))] attribute in Unity ensures 
        that the GameObject this script is attached to also has a CharacterController 
        component. If the GameObject doesn't already have one, Unity will automatically add it.
        This helps prevent errors and ensures that your script has everything it 
        needs to work properly.
     */
    /*
        You can also have [DisallowMultipleComponent]: Prevents multiple instances of the component from being added to a GameObject.
     */
    #endregion
    [RequireComponent(typeof(CharacterController))]
    #region Class and Inherited Behaviours
    /*
        In Unity, public class YourClassName : MonoBehaviour means you're creating a new 
        script that can be attached to game objects. public class defines a new class, 
        making it accessible to other scripts. : MonoBehaviour makes your class a Unity script, 
        allowing it to use Unity-specific functions like Start(), Update(), 
        and other built-in methods for game behavior.
     */
    #endregion
    public class Movement : MonoBehaviour
    {
        #region Attributes for the inspector
        /*
            C# Attributes in Unity! - Intermediate Scripting Tutorial
            https://youtu.be/HvAIvAEjSGU?si=mEXd8PDfSC5HcWJa
            Unity Manual - Attributes
            https://docs.unity3d.com/Manual/Attributes.html
        
            [SerializeField] makes private fields visible and editable in the Inspector.
            [Header("This is a title")] adds a labeled header above a field in the Inspector.
            [Space(10)] adds some spacing between fields in the Inspector.
            [Tooltip("Hover Description")] shows a helpful description when you hover over a field in the Inspector.
            [Min(minValue)]: Ensures the value of the field is never less than the specified minimum value.
            [ContextMenuItem("FunctionName", "ContextMenuTitle")]: Adds a context menu item to the field in the Inspector, 
            allowing you to call a specified function.
            [TextArea(minLines, maxLines)]: Similar to [Multiline], but you can specify the minimum and maximum lines for the text area.
            [Multiline]: Makes a string field display as a multi-line text area in the Inspector.
            [HideInInspector]: Hides a public field in the Inspector.
            [Range(min, max)]: Allows you to set a range for a numeric field, which creates a slider in the Inspector.
            These attributes make it easier to organize and understand your script settings in the Unity Editor.
         */
        #endregion
        #region Variables
        // These attributes customize how the _moveDirection field is displayed in the Unity Inspector.
        [SerializeField, Header("This is a title"), Space(10), Tooltip("Hover Description")]
        // This field stores the direction the character will move in, initialized to zero.
        Vector3 _moveDirection = Vector3.zero;
        // [SerializeField] makes these fields visible in the Inspector for easy tweaking.
        [SerializeField]
        // This field determines the character's movement speed.
        float _moveSpeed;
        // These fields set different movement speeds for walking, sprinting, and crouching.
        [SerializeField]
        float _walk = 5, _sprint = 10, _crouch = 2.5f;
        // This field sets the jump force.
        [SerializeField]
        float _jump = 8;
        // This field sets the gravity force applied to the character.
        [SerializeField] float _gravity = 20;
        // This field holds a reference to the CharacterController component attached to the GameObject.
        [SerializeField] CharacterController _characterController;
        #endregion
        #region Function
        private void Awake()
        {
            // This line gets the CharacterController component attached to this GameObject and assigns it to the _characterController field.
            _characterController = this.GetComponent<CharacterController>();
            // This line sets the initial movement speed to the walking speed.
            _moveSpeed = _walk;
            // This line ensures the script is enabled and will run its Update method.
            this.enabled = true;
        }
        private void Update()
        {
            if (Example.gameState == GAMESTATE.Game)
            {
                // This checks if the _characterController is not null (i.e., it exists).
                if (_characterController != null)
                {
                    // This checks if the character is on the ground.
                    if (_characterController.isGrounded)
                    {
                        //Runs the Behavouir Container that allows us to change speed based on user input
                        SpeedControl();
                        // This sets _moveDirection based on player input from the Horizontal and Vertical axes.
                        _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                        // This multiplies the direction by the current movement speed.
                        _moveDirection *= _moveSpeed;
                        // This checks if the player has pressed the Jump button.
                        if (Input.GetButton("Jump"))
                        {
                            // This sets the y component of _moveDirection to the jump force, making the character jump.
                            _moveDirection.y = _jump;
                        }
                    }
                    // This applies gravity to the y component of _moveDirection over time.
                    _moveDirection.y -= _gravity * Time.deltaTime;
                    // This moves the character based on _moveDirection, multiplied by Time.deltaTime for smooth movement.
                    _characterController.Move(_moveDirection * Time.deltaTime);
                }
            }
           
        }
        #endregion
        void SpeedControl()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _moveSpeed = _sprint;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                _moveSpeed = _crouch;
            }
            else
            {
                _moveSpeed = _walk;
            }
        }
    }
}

