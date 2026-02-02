using System;
using System.Collections.Generic;
using UnityEngine;

//Open–Closed Principle（对扩展开放，对修改关闭）
//存取与房间切换相关的功能
public class ChangeRoomSystem : Singleton<ChangeRoomSystem>
{
    // private Dictionary<GridType, Action> actions;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    actions = new()
    //    {
    //        { GridType.Enemy, EnterBattle },
    //        { GridType.Shop,  EnterShop },
    //        { GridType.Rest,  EnterRest },
    //    };
    //}

    public void EnterRoom(MapGrid grid)
    {
        //重构成基于RoomDataBase的数据驱动
        //if (grid.gridType == GridType.Enemy)
        //{
        //    GameManager.Instance.EnemyPool.SetEnemiesBuffer(grid.roomEnemies);
        //}

        //if (actions.TryGetValue(grid.gridType, out var act))
        //    act.Invoke();


        //OPTIMIZE: 基于RoomDataBase的数据驱动
        RoomDataBase.GetRoomData(grid.gridType).Enter(grid);

        //更新关卡数据
        GameManager.Instance.LevelProgress.IncreaseRound();
    }

    //private void EnterBattle() => GameManager.Instance.ToBattleMode();
    //private void EnterShop() => GameManager.Instance.ToShopMode();
    //private void EnterRest() => GameManager.Instance.ToRestMode();
}