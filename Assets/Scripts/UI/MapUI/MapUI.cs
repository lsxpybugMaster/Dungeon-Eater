using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour 
{
    public ChangeRoomButton changeRoomButton;

    private void OnEnable()
    {
        //现在由事件总线管理
        EventBus.Subscribe<RoomChangedEvent>(UpdateRoomButton);
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<RoomChangedEvent>(UpdateRoomButton);
    }

    //调用链: MapDiceSystem => RoomChangedEvent => UpdateRoomButton => ChangeRoomSystem.EnterRoom
    private void UpdateRoomButton(RoomChangedEvent e)
    {
        var g = e.grid;
        changeRoomButton.Setup($"[{g.gridIndex}] {g.gridType}", g);
    }
}
