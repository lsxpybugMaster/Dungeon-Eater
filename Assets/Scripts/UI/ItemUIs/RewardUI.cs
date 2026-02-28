using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


//仿照CardUI的控件, 不具有卡牌功能,理解为按钮
//不能只使用静态的Data数据,还需要动态的Context数据(内含静态Data)
public class RewardUI : ItemUI<RewardContext>
{
    //子类特有的相关属性
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text smallIcon;
    [SerializeField] private Image IconImg;

    protected override void Refresh(RewardContext data)
    {
        //初始化静态数据
        var rewardData = data.rewardData;

        //区分, 父类的image是指鼠标悬浮时修改的背景图
        IconImg.sprite = rewardData.Image;

        title.text = rewardData.Title;
        smallIcon.text = rewardData.SmallIcon;

        //根据动态数据, 动态解析字符串 
        description.text = data.Description;
    }

    protected override void MouseEnterEffect()
    {
        base.MouseEnterEffect();
        if (IconImg)
            IconImg.color = hoverColor;
    }

    protected override void MouseExitEffect()
    {
        base.MouseExitEffect();
        if (IconImg)
            IconImg.color = originColor;
    }
}
