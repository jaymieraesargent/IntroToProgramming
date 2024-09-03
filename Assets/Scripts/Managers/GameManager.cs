using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        //if we have no connection for our game manager instance
        if (instance == null)
        {
            //connect this as our connection 
            instance = this;
        }
        //otherwise if we have a connection and the connection isnt this copy
        else if (instance != null && instance != this)
        {
            //destroy/remove from game the imposter wannabe
            Destroy(this);
            //Destroy(this.gameObject);
        }
        ChangeGameState(GameState.Game);
    }
    #endregion
    public GameState currentGameState;

    public void ChangeGameState(GameState state)
    {
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.Game:
                //Lock the cursor to the middle of the screen
                Cursor.lockState = CursorLockMode.Locked;
                //Hide the Cursor from view
                Cursor.visible = false;
                break;
            case GameState.Pause:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
            case GameState.Menu:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameState.Death:
                //Lock the cursor to the middle of the screen
                Cursor.lockState = CursorLockMode.Locked;
                //Hide the Cursor from view
                Cursor.visible = false;
                break;
            default:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeGameState(GameState.Pause);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChangeGameState(GameState.Game);
        }
    }
}
public enum GameState
{
    Game,
    Pause,
    Menu,
    Death
}
