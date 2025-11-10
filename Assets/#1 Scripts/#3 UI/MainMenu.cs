using System;
using System.Collections;
using DependencyInjection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

class MainMenu : MonoBehaviour
{
    public GameObject StartBtn;
    public GameObject NormalBtn;
    public GameObject SpeedrunBtn;

    public Transform Start_Target;
    public Transform Normal_Target;
    public Transform Speedrun_Target;

    private float duration = 0.235f;
    private Ease ease = Ease.OutCubic;
    private bool StartButtonToggle = false;
    
    [Inject] private MoveUI _moveUI;

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
        
    }
    
    private void ClickSpeedrunBtn()
    {
        
    }
}