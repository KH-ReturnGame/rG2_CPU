using System;
using System.Collections;
using DependencyInjection;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerColide : MonoBehaviour
{
    [Inject] private Player player;

    [Inject] private PlayerMovement _playerMovement;
    
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
    
    
    private IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Debug.Log("wtfsdfasdf");
        Vector3 startPosition = _playerMovement._nowRigidbody.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // 시간 비율 계산
            Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, t); // 위치 보간
            _playerMovement._nowRigidbody.MovePosition(new Vector2(newPosition.x, _playerMovement._nowRigidbody.position.y)); // Y축은 유지하고 X축만 이동

            yield return null; // 다음 프레임까지 대기
        }

        // 최종적으로 정확한 타겟 위치로 이동
        _playerMovement._nowRigidbody.MovePosition(new Vector2(targetPosition.x, _playerMovement._nowRigidbody.position.y));
        
        player.RemoveState(PlayerStats.Push);
        //Debug.Log("ㄴㄴ");
        // foreach (Collider2D col in colliders)
        // {
        //     Physics2D.IgnoreCollision(col, dcol, false);
        // }
    }
}