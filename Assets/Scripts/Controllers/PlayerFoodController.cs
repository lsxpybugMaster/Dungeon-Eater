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

        if (data.Type == FoodType.Immediate)
        {
            Debug.Log("立即执行相关代码::");
            data.ImmediateData.OnPickup(); //调用多态方法
        }
        else
        {
            Debug.Log("更新天赋系统");
        }

        Raise_AddToPlayer(data); //激活事件

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
