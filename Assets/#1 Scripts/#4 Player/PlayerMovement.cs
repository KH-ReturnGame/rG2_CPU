using System;
using System.Collections.Generic;
using DependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //주입 받기
    [Inject] private Player player;
    [Inject] private GameState gameState;
    
    //움직임 정보
    private float _movementInputDirection;
    public float movementSpeed;
    public float rotationSpeed;
    public float jumpForce;
    
    //플레이어 정보
    private Rigidbody2D _nowRigidbody;
    public List<Rigidbody2D> playerRigidbody;

    private void Start()
    {
        _nowRigidbody = playerRigidbody[(int)gameState.controlObj];
    }

    private void FixedUpdate()
    {
        if (gameState.controlObj == ControlableObj.Head)
        {
            // 입력 방향에 따라 목표 각속도 설정 (예: 오른쪽 입력 → 시계 방향 회전)
            float targetAngularVelocity = -_movementInputDirection * rotationSpeed * 100;
        
            // 바로 적용 (즉시 반응 원하면 이렇게)
            _nowRigidbody.angularVelocity = targetAngularVelocity;
        
            // 또는 부드럽게 보간하고 싶으면 아래처럼 (선택사항)
            // _nowRigidbody.angularVelocity = Mathf.Lerp(_nowRigidbody.angularVelocity, targetAngularVelocity, rotationSmoothness * Time.fixedDeltaTime);
        }
        else if (gameState.controlObj == ControlableObj.Body)
        {
            _nowRigidbody.linearVelocity = new Vector2(_movementInputDirection * movementSpeed, _nowRigidbody.linearVelocity.y);
        }
    }
    
    public void OnChangeControlObj(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            gameState.controlObj += 1;
            if ((int)gameState.controlObj >= System.Enum.GetValues(typeof(ControlableObj)).Length)
            {
                gameState.controlObj = 0;
            }
            _nowRigidbody = playerRigidbody[(int)gameState.controlObj];
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
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInputDirection = context.ReadValue<Vector2>().x;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (((int)gameState.controlObj==0 && player.IsContainState(PlayerStats.BodyIsGround))||
                ((int)gameState.controlObj==1 && player.IsContainState(PlayerStats.HeadIsGround)))
            {
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, 0);
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, jumpForce);
            }
        }
    }
}