using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/EventRoomData")]
public class EventRoomData : RoomData
{
    public override void Enter(MapGrid grid)
    {
        GameManager.Instance.SceneModeManager.ToEventMode();
    }
}
