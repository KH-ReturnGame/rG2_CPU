using DependencyInjection;
using UnityEngine;

public enum ControlableObj
{
    Body = 0,
    Head,
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
    public bool isTutorial = false;
    public bool isSpeedrun = false;
    
    [Header("PlayerInfo")]
    public ControlableObj controlObj = ControlableObj.Body;
    public bool isControlArrow = false;
    
    [Header("SceneInfo")]
    public targetState targetScene = targetState.InGame;
    public int targetArea = 0;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}