using System;
using DependencyInjection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColide : MonoBehaviour
{
    [Inject] Player player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        tag = gameObject.tag;

        if (other.transform.CompareTag("Ground"))
        {
            switch (tag)
            {
                case "Head":
                    player.AddState(PlayerStats.HeadIsGround);
                    break;
                case "Body":
                    player.AddState(PlayerStats.BodyIsGround);
                    break;
                case "Arrow":
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        tag = gameObject.tag;

        if (other.transform.CompareTag("Ground"))
        {
            switch (tag)
            {
                case "Head":
                    player.RemoveState(PlayerStats.HeadIsGround);
                    break;
                case "Body":
                    player.RemoveState(PlayerStats.BodyIsGround);
                    break;
                case "Arrow":
                    break;
                default:
                    Debug.Log("default");
                    break;
            }
        }
    }
}