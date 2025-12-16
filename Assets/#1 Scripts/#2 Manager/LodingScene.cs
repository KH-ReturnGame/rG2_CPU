using System;
using System.Collections;
using DependencyInjection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LodingScene : MonoBehaviour
{
    // GameState 주입
    [Inject] private GameState _gameState;
    
    // 씬 목록들 (Editor only)
#if UNITY_EDITOR
    public SceneAsset Tutorial_Scene;
    public SceneAsset MainMenu_Scene;
    public SceneAsset[] Area_Scenes;
#endif
    
    // 런타임용 씬 이름들
    [HideInInspector, SerializeField]
    private string _tutorialSceneName;
    [HideInInspector, SerializeField]
    private string _mainMenuSceneName;
    [HideInInspector, SerializeField]
    private List<string> _areaSceneNames = new List<string>();
    
    // FillAmount Img
    public Image lodingImg;

    public void Start()
    {
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = null;
        
        if (_gameState.isTutorial)
        {
            asyncLoad = SceneManager.LoadSceneAsync(_tutorialSceneName, LoadSceneMode.Single);
        }
        else if (_gameState.targetScene == GameState.targetState.MainMenu)
        {
            asyncLoad = SceneManager.LoadSceneAsync(_mainMenuSceneName, LoadSceneMode.Single);
        }
        else if (_gameState.targetScene == GameState.targetState.InGame)
        {
            if (_gameState.targetArea >= 0 && _gameState.targetArea < _areaSceneNames.Count)
            {
                asyncLoad = SceneManager.LoadSceneAsync(_areaSceneNames[_gameState.targetArea], LoadSceneMode.Single);
            }
            else
            {
                Debug.LogError($"Invalid targetArea index: {_gameState.targetArea}");
                yield break;
            }
        }
        else
        {
            Debug.LogError("Error to load scene - invalid targetScene state");
            yield break;
        }
        
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = false;
        
            float timer = 0f;
            while (true)
            {
                yield return null;
                if (asyncLoad.progress < 0.8f)
                {
                    lodingImg.fillAmount = asyncLoad.progress;
                }
                else
                {
                    timer += Time.deltaTime;
                    lodingImg.fillAmount = Mathf.Lerp(0.8f, 1f, timer);
                    if (lodingImg.fillAmount >= 1f)
                    {
                        asyncLoad.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Tutorial Scene
        if (Tutorial_Scene != null)
            _tutorialSceneName = Tutorial_Scene.name;
        
        // MainMenu Scene
        if (MainMenu_Scene != null)
            _mainMenuSceneName = MainMenu_Scene.name;
        
        // Area Scenes
        if (_areaSceneNames == null)
            _areaSceneNames = new List<string>();
        
        _areaSceneNames.Clear();
        
        if (Area_Scenes != null)
        {
            foreach (var scene in Area_Scenes)
            {
                if (scene != null)
                    _areaSceneNames.Add(scene.name);
            }
        }
    }
#endif
}