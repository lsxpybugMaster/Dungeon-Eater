using UnityEngine;
using UnityEngine.UI;

public class UpdateCardMenu : AnimatedUI
{
    public override UILayer Layer => UILayer.DynamicUI;

    // 由两个子部分构成: UI显示部分 + 卡牌操作显示部分

    [SerializeField] private ShowDeckUI showDeckUI; 

    [SerializeField] private Button backAndHideBtn;

    private void Start()
    {
        backAndHideBtn.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    //展示该UI时,直接执行相关的初始化
    protected override void OnShow()
    {
        base.OnShow();
        //读取玩家卡牌并显示
        showDeckUI.Show();
    }
}
