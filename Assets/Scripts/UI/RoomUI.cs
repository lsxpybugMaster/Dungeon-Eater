using UnityEngine.UI;

/// <summary>
/// RoomUI 由 <see cref="RoomUIManager"/> 管理
/// </summary>
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
