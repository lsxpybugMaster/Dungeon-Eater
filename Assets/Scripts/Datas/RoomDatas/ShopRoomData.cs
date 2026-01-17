using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/ShopRoomData")]
public class ShopRoomData : RoomData
{
    public override void Enter(MapGrid grid)
    {
        GameManager.Instance.SceneModeManager.ToShopMode();
    }
}
