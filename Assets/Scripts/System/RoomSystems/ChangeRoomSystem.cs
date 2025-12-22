using System;
using System.Collections.Generic;
using UnityEngine;

//存取与房间切换相关的功能
public class ChangeRoomSystem : Singleton<ChangeRoomSystem>
{
    private Dictionary<GridType, Action> actions;

    protected override void Awake()
    {
        base.Awake();
        actions = new()
        {
            { GridType.Enemy, EnterBattle },
            { GridType.Shop,  EnterShop },
        };
    }

    public void EnterRoom(GridType type)
    {
        if (actions.TryGetValue(type, out var act))
            act.Invoke();
    }

    private void EnterBattle() => GameManager.Instance.ToBattleMode();
    private void EnterShop() => GameManager.Instance.ToShopMode();
}