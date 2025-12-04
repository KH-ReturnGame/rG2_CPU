using System;
using DependencyInjection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColide : MonoBehaviour
{
    [Inject] Player player;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        tag = gameObject.tag;
        
        if (other.transform.CompareTag("Ground") || other.transform.CompareTag("Body") || other.transform.CompareTag("Head"))
        {
            switch (tag)
            {
                case "Head":
                    player.AddState(PlayerStats.HeadIsGround);
                    break;
                case "Body":
                    player.AddState(PlayerStats.BodyIsGround);
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        tag = gameObject.tag;

        if (other.transform.CompareTag("Ground") || other.transform.CompareTag("Body") || other.transform.CompareTag("Head"))
        {
            switch (tag)
            {
                case "Head":
                    player.RemoveState(PlayerStats.HeadIsGround);
                    break;
                case "Body":
                    player.RemoveState(PlayerStats.BodyIsGround);
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }
}