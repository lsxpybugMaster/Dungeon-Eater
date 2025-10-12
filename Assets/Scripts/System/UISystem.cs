
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class UISystem : Singleton<UISystem>
{
    [SerializeField] private CanvasGroup victoryUI;
    [SerializeField] private Button backToMapBtn;

    //NOTE: ��������UI��ϵͳ
    [SerializeField] private CardPileCountUI cardPileCountUI;

    //�ж�UI�ܷ񽻻�����֤����
    public bool CanInteract() => GameManager.Instance.GameState != GameState.BattleVictory;

    //ͳһע����Ҫ����UI���¼�
    private void OnEnable()
    {
        CardSystem.Instance.OnPileChanged += OnPileCountChanged;
    }

    private void OnDisable()
    {
        //IMPORTANT: �пշ���
        if (CardSystem.Instance)
            CardSystem.Instance.OnPileChanged -= OnPileCountChanged;
    }

    private void Start()
    {
        //���ص���ͼ�İ�ť�󶨺���
        backToMapBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.ToMapScene();
        });        
    }

    public void OnPileCountChanged(int drawCount, int disCardCount)
    {
        cardPileCountUI.UpdataPileUI(drawCount, disCardCount);
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
