using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RoomData : ScriptableObject, IHaveKey<GridType>
{
    [field: SerializeField] public GridType RoomType { get; private set; }
    //之后会改成Sprite
    [field: SerializeField] public Color RoomColor { get; private set; }

    public GridType GetKey() => RoomType;

    //决定进入该房间前的逻辑
    public abstract void Enter(MapGrid grid);
}
