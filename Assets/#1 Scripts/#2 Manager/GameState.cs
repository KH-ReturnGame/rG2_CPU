using DependencyInjection;
using UnityEngine;

public class GameState : Singleton<GameState>, IDependencyProvider
{
    [Provide]
    public GameState ProvideGameState()
    {
        return this;
    }
    
    public enum targetState
    {
        MainMenu = 0,
        InGame,
    }

    [Header("PlayInfo")] 
    public int stage = 0;
    public string playType = "normal";
    public bool isTutorial = true;
    public targetState targetScene = targetState.InGame;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}