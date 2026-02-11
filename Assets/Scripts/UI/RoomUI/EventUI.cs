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
        EventBus.Subscribe<EEvent1>(OnEEvent1);
        EventBus.Subscribe<EEvent2>(OnEEvent2);
    }

    private void OnDisable()
    {
        EventBus.UnSubscribe<EEvent1>(OnEEvent1);
        EventBus.UnSubscribe<EEvent2>(OnEEvent2);
    }

    private void OnEEvent1(EEvent1 e)
    {
        StartCoroutine(Wait());
        Debug.Log($"事件EEvent1激活 : {e.x}");
        
    }
    private void OnEEvent2(EEvent2 e)
    {
        StartCoroutine(Wait());
        Debug.Log($"事件EEvent2激活 : {e.y}");
        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }

    protected override void OnShow()
    {
        base.OnShow();
        EnableButtons();
        initEvent();
    }

    private void EnableButtons()
    {
        //隐藏退出按钮
        btn.gameObject.SetActive(false);

        for (int i = 0; i < eventChoiceBtns.Count; i++)
        {
            eventChoiceBtns[i].EnableButton();
        }
    }

    private void DisableButtons()
    {
        for (int i = 0; i < eventChoiceBtns.Count; i++)
        {
            eventChoiceBtns[i].DisableButton();
        }
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
            //防止出现闭包
            EditableEvents _event = eve.EventChoice[i].ClickEvent;

            //绑定函数
            eventChoiceBtns[i].AddListenerOnClick(
                () => {
                    DisableButtons();
                    EventBus.Publish_Dynamic(_event); //注意需要动态调用Dynamic_Event
                    //重新显示退出按钮
                    btn.gameObject.SetActive(true);
                }
            );
        }
    }
}
