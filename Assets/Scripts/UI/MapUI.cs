using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUI : Singleton<MapUI>
{
    [SerializeField] private Button swtichModeBtn;
    [SerializeField] private TMP_Text modeInfoTMP;

    //委托 void func() 函数, “指向”点击逻辑函数 
    private Action currentButtonClickAction;

    private void OnEnable()
    {
        swtichModeBtn.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        swtichModeBtn.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        currentButtonClickAction();
    }

    public void BindClickAction(string settings, Action onClick)
    {
        modeInfoTMP.text = settings;
        currentButtonClickAction = onClick;
    }
}
