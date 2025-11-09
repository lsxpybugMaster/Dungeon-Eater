using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//存储与Map逻辑有关的事件结构体,用于EventBus
public struct RoomChangedEvent
{
    public GridType gridType;

    public RoomChangedEvent(GridType gridType)
    {
        this.gridType = gridType;
    }
}