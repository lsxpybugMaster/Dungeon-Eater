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
        ChangeRoomSystem.OnRoomChanged += BindClickAction;
        swtichModeBtn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        ChangeRoomSystem.OnRoomChanged -= BindClickAction;
        swtichModeBtn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        currentButtonClickAction?.Invoke();
    }

    public void BindClickAction(string settings, Action onClick)
    {
        Debug.Log("settings");
        modeInfoTMP.text = settings;
        currentButtonClickAction = onClick;
    }
}
