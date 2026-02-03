using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestUI : RoomUI
{
    //[SerializeField] private ShowDeckUI showDeckUI;

    //按钮组合
    [SerializeField] private Button updateCardChoiceBtn;
    [SerializeField] private Button deleteCardChoiceBtn;

    // 在 Start 中注册按钮, 避免出现反复激活时反复注册按钮的情况
    private void Start()
    {
        updateCardChoiceBtn.onClick.AddListener(() =>
        {
            DynamicUIManager.Instance.updateCardMenu.Show();
        });

        deleteCardChoiceBtn.onClick.AddListener(() =>
        {

        });
        //showDeckUI.Show();
    }
}
