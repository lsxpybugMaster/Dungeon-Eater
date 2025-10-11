
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class UISystem : Singleton<UISystem>
{
    [SerializeField] private CanvasGroup victoryUI;
    [SerializeField] private Button backToMapBtn;

    //判断UI能否交互的验证函数
    public bool CanInteract() => GameManager.Instance.GameState != GameState.BattleVictory;

    private void Start()
    {
        //给回到地图的按钮绑定函数
        backToMapBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.ToMapScene();
        });        
    }

    public void Show()
    {
        victoryUI.gameObject.SetActive(true);
        victoryUI.alpha = 1.0f;
        victoryUI.blocksRaycasts = true;
        victoryUI.interactable = true;
    }

    public void Hide()
    {
        victoryUI.alpha = 0f;
        victoryUI.blocksRaycasts = false;
        victoryUI.interactable = false;
        victoryUI.gameObject.SetActive(false);
    }

    public void ShowWinUI()
    {
        Show();
    }
}
