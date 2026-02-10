using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : RoomUI
{
    [SerializeField] private TMP_Text eventTitle;
    [SerializeField] private TMP_Text eventDescription;
    [SerializeField] private List<EventChoiceButton> eventChoiceBtns;

    private void OnEnable()
    {
        EventBus.Subscribe<EditableEvents>(OnEvent);
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<EditableEvents>(OnEvent);
    }

    private void OnEvent(EditableEvents e)
    {
        Debug.Log($"事件激活 : {e.x}");
    }

    protected override void OnShow()
    {
        base.OnShow();
        initEvent();
    }

    private void initEvent()
    {
        //随机从数据库获取事件
        EventData eve = EventDataBase.GetRandomEvent();
        eventTitle.text = eve.EventName;
        eventDescription.text = eve.EventDescription;

        List<EventChoice> eventChoices = eve.EventChoice;
        for (int i = 0; i < eventChoices.Count; i++)
        {
            eventChoiceBtns[i].ButtonText = eve.EventChoice[i].ChoiceInfo;
            eventChoiceBtns[i].Btn.onClick.RemoveAllListeners();
            //eventChoiceBtns[i].RemoveAndAddListenerOnClick(
            //    //() => EventBus.Subscribe<>()
            //);
        }
    }
}
