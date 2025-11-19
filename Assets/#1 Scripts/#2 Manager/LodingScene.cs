using System;
using System.Collections;
using DependencyInjection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LodingScene : MonoBehaviour
{
    [Inject] private GameState _gameState;
    
    public void Awake()
    {
        Debug.Log(_gameState.targetScene +" / "+ _gameState.isTutorial);
    }

    public void Start()
    {
        StartCoroutine("LoadScene");
    }

    IEnumerator LoadScene()
    {
        yield return SceneManager.LoadSceneAsync("SceneLoad", LoadSceneMode.Single);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextScene,_nextMode);
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = false;
        
            float timer = 0f;
            while (true)
            {
                yield return null;
                //Debug.Log(asyncLoad.progress);
                if (asyncLoad.progress < 0.9f)
                {
                    GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount = asyncLoad.progress;
                }
                else
                {
                    timer += Time.deltaTime;
                    GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount =
                        Mathf.Lerp(0.9f, 1f, timer);
                    if (GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount >= 1f)
                    {
                        asyncLoad.allowSceneActivation = true;
                        Debug.Log("Scene loaded: " + _nextScene);
                        GameManager.Instance.isLoding = false;
                        yield break;
                    }
                }
            }
        }
    }
    

    // IEnumerator LoadScene()
    // {
    //     GameManager.Instance.isLoding = true;
    //     yield return SceneManager.LoadSceneAsync("SceneLoad", LoadSceneMode.Single);
    //     
    //     AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_nextScene,_nextMode);
    //     if (asyncLoad != null)
    //     {
    //         asyncLoad.allowSceneActivation = false;
    //
    //         float timer = 0f;
    //         while (true)
    //         {
    //             yield return null;
    //             //Debug.Log(asyncLoad.progress);
    //             if (asyncLoad.progress < 0.9f)
    //             {
    //                 GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount = asyncLoad.progress;
    //             }
    //             else
    //             {
    //                 timer += Time.deltaTime;
    //                 GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount =
    //                     Mathf.Lerp(0.9f, 1f, timer);
    //                 if (GameObject.FindGameObjectWithTag("loding").GetComponent<Image>().fillAmount >= 1f)
    //                 {
    //                     asyncLoad.allowSceneActivation = true;
    //                     Debug.Log("Scene loaded: " + _nextScene);
    //                     GameManager.Instance.isLoding = false;
    //                     yield break;
    //                 }
    //             }
    //         }
    //     }
    // }
}