using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestUI : RoomUI
{
    //[SerializeField] private ShowDeckUI showDeckUI;
    private UpdateCardMenu updateCardMenu;
    private UpdateCardMenu deleteCardMenu;

    //按钮组合
    [SerializeField] private Button updateCardChoiceBtn;
    [SerializeField] private Button deleteCardChoiceBtn;

    protected override void OnShow()
    {
        base.OnShow();
        ShowAllBtns();

        updateCardMenu = DynamicUIManager.Instance.updateCardMenu;
        deleteCardMenu = DynamicUIManager.Instance.deleteCardMenu;

        //注册相关的函数,使得调用的Menu退出时自己也跟着退出
        updateCardMenu.OnMenuHide += OnExit;
        deleteCardMenu.OnMenuHide += OnExit;
    }

    protected override void OnExit()
    {
        base.OnExit();
        //注意及时取消注册
        updateCardMenu.OnMenuHide -= OnExit;
        deleteCardMenu.OnMenuHide -= OnExit;
    }

    // 在 Start 中注册按钮, 避免出现反复激活时反复注册按钮的情况
    private void Start()
    {
        updateCardChoiceBtn.onClick.AddListener(() =>
        {
            updateCardMenu.Show();
            HideAllBtns();
        });

        deleteCardChoiceBtn.onClick.AddListener(() =>
        {
            deleteCardMenu.Show();
            HideAllBtns();
        });
        //showDeckUI.Show();
    }

    private void ShowAllBtns()
    {
        updateCardChoiceBtn.gameObject.SetActive(true);
        deleteCardChoiceBtn.gameObject.SetActive(true);
    }

    private void HideAllBtns()
    {
        updateCardChoiceBtn.gameObject.SetActive(false);
        deleteCardChoiceBtn.gameObject.SetActive(false);
    }
}
