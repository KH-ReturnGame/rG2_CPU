using System;
using System.Collections;
using DependencyInjection;
using DG.Tweening;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class MainMenu : MonoBehaviour
{
    // 버튼 오브젝트
    public GameObject StartBtn;
    public GameObject NormalBtn;
    public GameObject SpeedrunBtn;

    public Transform Start_Target;
    public Transform Normal_Target;
    public Transform Speedrun_Target;

    // 애니메이션 관련 설정
    private float duration = 0.235f;
    private Ease ease = Ease.OutCubic;
    private bool StartButtonToggle = false;
    
    // 주입
    [Inject] private MoveUI _moveUI;
    [Inject] private GameState _gameState;

    // 이동할 씬 가져오기 (Editor only)
#if UNITY_EDITOR
    public SceneAsset Loding_Scene;
#endif
    
    // 런타임용 씬 이름
    [HideInInspector, SerializeField]
    private string _lodingSceneName;
    
    
    
    private void Start()
    {
        StartBtn.GetComponent<Button>().onClick.AddListener(ClickStartBtn);
        NormalBtn.GetComponent<Button>().onClick.AddListener(ClickNormalBtn);
        SpeedrunBtn.GetComponent<Button>().onClick.AddListener(ClickSpeedrunBtn);
        NormalBtn.SetActive(false);
        SpeedrunBtn.SetActive(false);
    }

    private void ClickStartBtn()
    {
        StartButtonToggle = !StartButtonToggle;
        StartCoroutine("Active");
        
        Vector3 originStartBtnPos = StartBtn.transform.position;
        Vector3 originNormalBtnPos = NormalBtn.transform.position;
        Vector3 originSpeedrunBtnPos = SpeedrunBtn.transform.position;
        
        _moveUI.Move(Start_Target,StartBtn, duration, ease);
        _moveUI.Move(Normal_Target,NormalBtn, duration, ease);
        _moveUI.Move(Speedrun_Target,SpeedrunBtn, duration*1.2f, ease);
        
        Start_Target.transform.position = originStartBtnPos;
        Normal_Target.transform.position = originNormalBtnPos;
        Speedrun_Target.transform.position = originSpeedrunBtnPos;
    }
    
    IEnumerator Active()
    {
        if (StartButtonToggle)
        {
            
            yield return new WaitForSeconds(0.03f);
            if (!NormalBtn.activeSelf && !SpeedrunBtn.activeSelf)
            {
                NormalBtn.SetActive(true);
                SpeedrunBtn.SetActive(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.025f);
            if (NormalBtn.activeSelf && SpeedrunBtn.activeSelf)
            {
                NormalBtn.SetActive(false);
                SpeedrunBtn.SetActive(false);
            }
        }
    }
    
    private void ClickNormalBtn()
    {
        _gameState.targetScene = GameState.targetState.InGame;
        SceneManager.LoadScene(_lodingSceneName);
    }
    
    private void ClickSpeedrunBtn()
    {
        _gameState.targetScene = GameState.targetState.InGame;
        SceneManager.LoadScene(_lodingSceneName);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (Loding_Scene != null)
            _lodingSceneName = Loding_Scene.name;
    }
#endif
}