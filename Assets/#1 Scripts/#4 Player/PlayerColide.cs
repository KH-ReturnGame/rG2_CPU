using System;
using DependencyInjection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColide : MonoBehaviour
{
    [Inject] Player player;
    
    private int head_collideCount;

    private int body_collideCount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        tag = gameObject.tag;
        
        if (other.transform.CompareTag("Ground") || other.transform.CompareTag("Body") || other.transform.CompareTag("Head"))
        {
            switch (tag)
            {
                case "Head":
                    if (!other.transform.CompareTag("Head"))
                    {
                        player.AddState(PlayerStats.HeadIsGround);
                        head_collideCount++;
                    }
                    break;
                case "Body":
                    if (!other.transform.CompareTag("Body"))
                    {
                        player.AddState(PlayerStats.BodyIsGround);
                        body_collideCount++;
                    }
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
                    if (!other.transform.CompareTag("Head"))
                    {
                        head_collideCount--;
                        if (head_collideCount == 0)
                        {
                            player.RemoveState(PlayerStats.HeadIsGround);
                        }
                    }
                    break;
                case "Body":
                    if (!other.transform.CompareTag("Body"))
                    {
                        body_collideCount--;
                        if (body_collideCount == 0)
                        {
                            player.RemoveState(PlayerStats.BodyIsGround);
                        }
                    }
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }
}