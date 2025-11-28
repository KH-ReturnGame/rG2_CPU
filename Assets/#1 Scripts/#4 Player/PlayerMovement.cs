using DependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //주입 받기
    [Inject] private Player player;
    [Inject] private GameState gameState;
    
    public void OnChangeControlObj(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            gameState.controlObj += 1;
            if ((int)gameState.controlObj >= System.Enum.GetValues(typeof(ControlableObj)).Length)
            {
                gameState.controlObj = 0;
            }
            Debug.Log(gameState.controlObj);
        }
        // else if (context.canceled)
        // {
        //     Debug.Log("Cancled");
        // }
        // else
        // {
        //     Debug.Log("None");
        // }
    }
}