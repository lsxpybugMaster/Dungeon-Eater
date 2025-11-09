using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理房间切换中间的逻辑,在玩家移动到新格子后执行
/// 主要由C# Action 字典组成
/// </summary>
public class ChangeRoomSystem : MonoBehaviour
{
    public static Dictionary<GridType, Action> actions = new Dictionary<GridType, Action>();

    //由事件总线管理
    //public static event Action<string,Action> OnRoomChanged;

    private void Awake()
    {
        // 只初始化一次
        if (actions.Count == 0)
        {
            actions[GridType.Enemy] = ChangeToBattleRoom;
            actions[GridType.Shop] = ChangeToShopRoom;
        }
    }

    public void ChangeToBattleRoom()
    {
        GameManager.Instance.ToBattleMode();
    }

    public void ChangeToShopRoom()
    {
        GameManager.Instance.ToShopMode();
    }

    public void ChangeToEventRoom()
    {
        //not implemented
    }
}
