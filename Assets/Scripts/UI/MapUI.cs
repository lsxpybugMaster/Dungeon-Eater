using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [SerializeField] private Button swtichModeBtn;
    [SerializeField] private TMP_Text modeInfoTMP;

    //委托 void func() 函数, “指向”点击逻辑函数 
    private Action currentButtonClickAction; 

    private void OnEnable()
    {
        //现在由事件总线管理
        //ChangeRoomSystem.OnRoomChanged += BindClickAction;
        EventBus.Subscribe<RoomChangedEvent>(OnRoomChanged);
        
        swtichModeBtn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<RoomChangedEvent>(OnRoomChanged);

        swtichModeBtn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        currentButtonClickAction?.Invoke();
    }

    //NOTE: 入口函数
    private void OnRoomChanged(RoomChangedEvent e)
    {
        MapGrid g = e.grid;

        Debug.Log($"INFO: {g.gridIndex}");

        GridType gridType = g.gridType;
        // BindClickAction(gridType.ToString(), ChangeRoomUtil.GridActions[gridType]);

        BindClickAction(gridType.ToString(), () => ChangeRoomSystem.Instance.Enter(gridType)) ;
    }

    public void BindClickAction(string settings, Action onClick)
    {
        modeInfoTMP.text = settings;
        currentButtonClickAction = onClick;
    }
}
