using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodController : PlayerItemController<FoodData>
{
    public PlayerFoodController(HeroState heroState) : base(heroState)
    {
    }

    public override int Size => heroState.FoodsSize;

    public override List<FoodData> Datas => heroState.Foods;


    /// <summary>
    /// 在这里执行与获取道具有关的逻辑, 包括获取逻辑和UI显示
    /// </summary>
    /// <param name="data"></param>
    public override void AddToPlayer(FoodData data)
    {
        heroState.AddFood(data);

        //根据道具的生效属性确定道具效果
        //立即道具: 立即执行, 之后不再有功能, 仅可显示
        if (data.Type == FoodType.Immediate)
        {
            data.ImmediateData.OnPickup(); //调用多态方法
        }
        //天赋道具: 记录在Perk中, 每次战斗中动态地读取 + 注册
        else
        {
            heroState.AddPerk(data.perkData);
        }

        //激活其他相关的外部事件
        Raise_AddToPlayer(data); 

        //更新 UI 系统
        PersistUIUpdate();
    }

    public override void RemoveFromPlayer(FoodData data)
    {
        //暂时不提供移除功能, 因为目前没有任何道具会被移除
    }

    public override void UpdateItems(FoodData data)
    {
        //无需提供升级功能
    }

    public override void PersistUIUpdate()
    {
        GameManager.Instance.PersistUIController.FoodUI.Show(heroState.Foods, isGroup: false);
    }
}
