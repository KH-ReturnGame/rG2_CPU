using DependencyInjection;
using UnityEngine;

public enum ControlableObj
{
    Body = 0,
    Head,
    Arrow,
}

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
    public int currentArea = 0;
    public string playType = "normal";
    public bool isTutorial = true;
    public bool isSpeedrun = false;
    public targetState targetScene = targetState.InGame;
    public int targetArea = 0;
    public ControlableObj controlObj = ControlableObj.Body;
    
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}