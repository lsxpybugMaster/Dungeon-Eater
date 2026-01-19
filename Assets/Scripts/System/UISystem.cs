
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class UISystem : Singleton<UISystem>
{
    [SerializeField] private CanvasGroup victoryUI;
    [SerializeField] private Button backToMapBtn;

    //NOTE: 引用其他UI子系统
    [SerializeField] private CardPileUI cardPileCountUI;

    [SerializeField] private GameObject failUI;

    //判断UI能否交互的验证函数
    public bool CanInteract() => GameManager.Instance.GameState != GameState.BattleVictory;

    //统一注册需要更新UI的事件
    private void OnEnable()
    {
        CardSystem.Instance.OnPileChanged += OnPileCountChanged;
    }

    private void OnDisable()
    {
        //IMPORTANT: 判空防御
        if (CardSystem.Instance)
            CardSystem.Instance.OnPileChanged -= OnPileCountChanged;
    }

    private void Start()
    {
        //给回到地图的按钮绑定函数
        backToMapBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.SceneModeManager.ToMapMode();
        });        
    }

    public void OnPileCountChanged(int drawCount, int disCardCount)
    {
        cardPileCountUI.UpdataPileUI(drawCount, disCardCount);
    }    

    public void ShowCanvasGroup()
    {
        victoryUI.gameObject.SetActive(true);
        victoryUI.alpha = 1.0f;
        victoryUI.blocksRaycasts = true;
        victoryUI.interactable = true;
    }

    public void HideCanvasGroup()
    {
        victoryUI.alpha = 0f;
        victoryUI.blocksRaycasts = false;
        victoryUI.interactable = false;
        victoryUI.gameObject.SetActive(false);
    }

    public void ShowWinUI()
    {
        ShowCanvasGroup();
    }

    public void ShowFailUI()
    {
        failUI.gameObject.SetActive(true);
    }
}
