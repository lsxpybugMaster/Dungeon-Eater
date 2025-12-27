using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUI : AnimatedUI
{
    public Button btn;

    public override UILayer Layer => throw new System.NotImplementedException();

    protected override void Awake()
    {
        base.Awake();
        btn.onClick.AddListener(OnExit);
    }

    protected virtual void OnExit()
    {
        RoomUIEvents.OnRoomExit?.Invoke();
    }
}
