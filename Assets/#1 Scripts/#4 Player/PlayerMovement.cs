using DependencyInjection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player 주입 받기
    [Inject] private Player player;
}