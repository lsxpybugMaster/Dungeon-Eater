using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/RestRoomData")]
public class RestRoomData : RoomData
{
    public override void Enter(MapGrid grid)
    {
        GameManager.Instance.ToRestMode();
    }
}
