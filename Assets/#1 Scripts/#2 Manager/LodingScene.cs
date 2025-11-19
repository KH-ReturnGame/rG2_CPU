using System;
using System.Collections;
using DependencyInjection;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LodingScene : MonoBehaviour
{
    // GameState 주입
    [Inject] private GameState _gameState;
    
    // 씬 목록들
    public SceneAsset Tutorial_Scene;
    public SceneAsset MainMenu_Scene;
    public SceneAsset[] Area_Scenes;
    
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
            asyncLoad = SceneManager.LoadSceneAsync(Tutorial_Scene.name, LoadSceneMode.Single);
        }
        else if (_gameState.targetScene == GameState.targetState.MainMenu)
        {
            asyncLoad = SceneManager.LoadSceneAsync(MainMenu_Scene.name, LoadSceneMode.Single);
        }
        else if (_gameState.targetScene == GameState.targetState.InGame)
        {
            asyncLoad = SceneManager.LoadSceneAsync(Area_Scenes[_gameState.targetArea].name, LoadSceneMode.Single);
        }
        else
        {
            Debug.Log("Error to load scene");
            yield return null;
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
                    lodingImg.fillAmount =
                        Mathf.Lerp(0.8f, 1f, timer);
                    if (lodingImg.fillAmount >= 1f)
                    {
                        asyncLoad.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}