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

        MapInteractions.OnButtonEnabled += EnableButton;
        MapInteractions.OnButtonDisabled += DisableButton;
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<RoomChangedEvent>(UpdateRoomButton);

        MapInteractions.OnButtonEnabled -= EnableButton;
        MapInteractions.OnButtonDisabled -= DisableButton;
    }

    //调用链: MapDiceSystem => RoomChangedEvent => UpdateRoomButton => ChangeRoomSystem.EnterRoom
    private void UpdateRoomButton(RoomChangedEvent e)
    {
        var g = e.grid;
        //重新显示按钮
        MapInteractions.OnButtonEnabled();
        changeRoomButton.Setup($"[{g.gridIndex}] {g.gridType}", g);
    }

    public void EnableButton()
    {
        changeRoomButton.Btn.gameObject.SetActive(true);
    }

    private void DisableButton()
    {
        changeRoomButton.Btn?.gameObject.SetActive(false);
    }

}
