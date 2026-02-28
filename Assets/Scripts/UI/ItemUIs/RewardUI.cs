using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//仿照CardUI的控件, 不具有卡牌功能,理解为按钮
//不能只使用静态的Data数据,还需要动态的Context数据(内含静态Data)
public class RewardUI : ItemUI<RewardContext>
{
    protected override void Refresh(RewardContext data)
    {
        //初始化静态数据
        var rewardData = data.rewardData;

        image.sprite = rewardData.Image;
    
        //初始化动态数据
    }
}
