using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理房间切换中间的逻辑,在玩家移动到新格子后执行
/// 主要由C# Action 字典组成
/// </summary>
public static class ChangeRoomUtil
{
    public static Dictionary<GridType, Action> GridActions { get; private set; } = new();

    //由事件总线管理
    //public static event Action<string,Action> OnRoomChanged;

    // 静态构造函数确保仅初始化一次
    static ChangeRoomUtil()
    {
        GridActions[GridType.Enemy] = ChangeToBattleRoom;
        GridActions[GridType.Shop] = ChangeToShopRoom;      
    }

    private static void ChangeToBattleRoom()
    {
        GameManager.Instance.ToBattleMode();
    }

    private static void ChangeToShopRoom()
    {
        GameManager.Instance.ToShopMode();
    }

    private static void ChangeToEventRoom()
    {
        //not implemented
    }
}
