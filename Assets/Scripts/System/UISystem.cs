using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UISystem : MonoBehaviour
{
    [SerializeField] private CanvasGroup victoryUI;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ShowWinUIGA>(ShowWinUIPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ShowWinUIGA>();
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

    private IEnumerator ShowWinUIPerformer(ShowWinUIGA showWinUIGA)
    {
        Show();
        yield return null;
    }
}
