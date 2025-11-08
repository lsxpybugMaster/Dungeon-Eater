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
    private static Dictionary<GridType, Action> actions = new Dictionary<GridType, Action>();

    public static event Action<string,Action> OnRoomChanged;

    private void Awake()
    {
        // 只初始化一次
        if (actions.Count == 0)
        {
            actions[GridType.Enemy] = ChangeToBattleRoom;
            actions[GridType.Shop] = ChangeToShopRoom;
        }
    }

    public static void ChangeRoom(GridType gridType)
    {
        Debug.Log($"Args: {gridType.ToString()},{actions[gridType]}");
        OnRoomChanged.Invoke(gridType.ToString(), actions[gridType]);
        //actions[gridType].Invoke();
        //mapUI.BindClickAction(gridType.ToString(), );
    }

    public void ChangeToBattleRoom()
    {
        Debug.Log("ChangeToBattleMode");
    }

    public void ChangeToShopRoom()
    {
        Debug.Log("ChangeToShopMode");
    }

    public void ChangeToEventRoom()
    {
        Debug.Log("ChangeToEventMode");
    }

}
