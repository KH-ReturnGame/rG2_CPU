using DependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Player 주입 받기
    [Inject] private Player player;
    
    public void OnChangeControlObj(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            Debug.Log("Start");
        }
        else if (context.canceled)
        {
            Debug.Log("Cancled");
        }
        else
        {
            Debug.Log("None");
        }

        Debug.Log("ㅗㅗㅗ");
    }
}