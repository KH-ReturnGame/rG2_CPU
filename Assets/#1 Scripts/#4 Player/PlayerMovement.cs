using System;
using System.Collections.Generic;
using DependencyInjection;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Singleton<PlayerMovement>, IDependencyProvider
{
    //주입 받기
    [Inject] private Player player;
    [Inject] private GameState gameState;
    
    //움직임 정보
    private float _movementInputDirection;
    public float movementSpeed;
    public float rotationSpeed;
    public float body_jumpForce;
    public float head_jumpForce;
    
    //플레이어 정보
    public Rigidbody2D _nowRigidbody;
    public List<Rigidbody2D> playerRigidbody;

    [Provide]
    public PlayerMovement ProvidePlayerMovement()
    {
        return this;
    }
    
    private void Start()
    {
        _nowRigidbody = playerRigidbody[(int)gameState.controlObj];
    }

    private void FixedUpdate()
    {
        if (gameState.controlObj == ControlableObj.Head)
        {
            if (!player.IsContainState(PlayerStats.HeadIsGround))
            {
                _nowRigidbody.linearVelocity = new Vector2(_movementInputDirection * movementSpeed,
                    _nowRigidbody.linearVelocity.y);
            }
            else
            {
                float targetAngularVelocity = -_movementInputDirection * rotationSpeed * 50;
                _nowRigidbody.angularVelocity = targetAngularVelocity;
            }
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
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInputDirection = context.ReadValue<Vector2>().x;
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if ((int)gameState.controlObj==0 && player.IsContainState(PlayerStats.BodyIsGround))
            {
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, 0);
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, body_jumpForce);
            }
            else if ((int)gameState.controlObj == 1 && player.IsContainState(PlayerStats.HeadIsGround))
            {
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, 0);
                _nowRigidbody.linearVelocity = new Vector2(_nowRigidbody.linearVelocity.x, head_jumpForce);
            }
        }
    }
}