using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 对应实现的部分: <see cref="ManaSystem.AddManaPerformer"/>
public class AddManaGA : GameAction {
    public int Amount { get; private set; }
    public bool Refill { get; private set; } = false;
    public AddManaGA(int amount, bool refill)
    {
        Amount = amount;
        Refill = refill;
    }
}

//根据枚举类索引增加资源点
/// 对应实现的部分: <see cref="ManaSystem.AddManaPerformer"/>
public class AddOtherManaGA : GameAction 
{
    public int Amount { get; private set; }
    public ManaID ManaID { get; private set; }

    public AddOtherManaGA(int amount, ManaID manaID)
    {
        Amount = amount;
        ManaID = manaID;
    }
}