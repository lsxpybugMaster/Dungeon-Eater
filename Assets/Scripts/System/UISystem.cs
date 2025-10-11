using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UISystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup victoryUI;

    private void OnEnable()
    {
        //ActionSystem.AttachPerformer<ShowWinUIGA>(ShowWinUIPerformer);
        //�����¼�
        ActionSystem.SubscribeReaction<KillAllEnemyGA>(ShowWinUI, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        //ActionSystem.DetachPerformer<ShowWinUIGA>();
        ActionSystem.UnsubscribeReaction<KillAllEnemyGA>(ShowWinUI, ReactionTiming.POST);
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

    private void ShowWinUI(KillAllEnemyGA killAllEnemyGA)
    {
        Show();
        // ��ʾ��UI������Ϸģʽ���Խ������� 
        //TODO: ���д����ְ���������ĺ����ͽű���ƥ��
        GameManager.Instance.ChangeGameState(GameState.BattleVictory);
    }

    //private IEnumerator ShowWinUIPerformer(ShowWinUIGA showWinUIGA)
    //{
    //    Show();
    //    yield return null;
    //}
}
