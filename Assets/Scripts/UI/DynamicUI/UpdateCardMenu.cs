using System;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCardMenu : AnimatedUI
{
    public override UILayer Layer => UILayer.DynamicUI;

    // 通知调用者该UI已经执行完毕
    public event Action OnMenuHide;

    // 由两个子部分构成: UI显示部分 + 卡牌操作显示部分

    [SerializeField] private ShowDeckUI showDeckUI;      //展示牌组信息UI
    [SerializeField] private ShowCardUIBase showCardUI;//展示卡牌选择结果UI(含逻辑)
    [SerializeField] private Button backAndHideBtn;       

    private void Start()
    {
        backAndHideBtn.onClick.AddListener(() =>
        {
            OnMenuHide?.Invoke();
            Hide();
        });
    }

    //展示该UI时,直接执行相关的初始化
    protected override void OnShow()
    {
        base.OnShow();
        //读取玩家卡牌并显示
        showDeckUI.Show();
        //重新初始化选中卡牌展示面板
        showCardUI.Setup();
    }
}
