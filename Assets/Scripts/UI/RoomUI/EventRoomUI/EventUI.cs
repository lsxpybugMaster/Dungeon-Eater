using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventUI : RoomUI
{
    [SerializeField] private EventResultUI eventResultUI;

    [SerializeField] private TMP_Text eventTitle;
    [SerializeField] private TMP_Text eventDescription;
    [SerializeField] private List<EventChoiceButton> eventChoiceBtns;
    public EventModel model { get; private set; }

    private new void Awake()
    {
        base.Awake();
        model = new EventModel(); //初始化model同时,进行事件注册
    }

    private void OnDestroy()
    {
        model.OnDestroy(); //及时调用model,取消事件注册
    }


    //处理按钮点击事件, 给其添加上下文
    //处理玩家选择的结果,依据结果分发事件
    private IEnumerator HandleEvent(EventChoice choice)
    {
        //处理事件前禁用按钮
        DisableButtons();

        EditableEvents evt;
        //判定事件是否需要检定
        if (model.ChoiceNeedCheck)
        {
            //检定
            int pt;
            Result result = CheckUtil.EventCheck(model.CheckPoint, 0, out pt);

            evt = result.IsSuccess() ? choice.SuccessEvent
                                     : choice.FailedEvent;

            //EventModel 作检定 EventResultUI做展示
            yield return eventResultUI.ShowEventResult(pt, model.CheckPoint, evt.EventResultInfo);
        }
        else //无需检定
        {
            evt = choice.SuccessEvent;
            //无需检定的显示效果不同
            yield return eventResultUI.ShowEventResult(evt.EventResultInfo);
        }

        yield return ActBus.Perform(evt);

        //协程处理完毕后, 将 back 按钮显示
        btn.gameObject.SetActive(true);
    }


    protected override void OnShow()
    {
        base.OnShow();   //RoomUI基类相应初始化
        EnableButtons(); //显示相关的事件按钮
        initEvent();     //初始化事件绑定以及展示
        OtherComponentInit(); //其管理的子对象的初始化
    }

    private void OtherComponentInit()
    {
        eventResultUI.OnShow();
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
        model.GenerateEvent(); 
        EventData eve = model.CurrentEvent;
        
        eventTitle.text = eve.EventName;
        eventDescription.text = eve.EventDescription;

        List<EventChoice> eventChoices = eve.EventChoice;
        for (int i = 0; i < eventChoices.Count; i++)
        {
            eventChoiceBtns[i].ButtonText = eve.EventChoice[i].ChoiceInfo;
           
            //防止出现闭包
            int idx = i;

            //绑定函数
            //注意与 ActionSystem 类似, 绑定的是一个协程, 在这里进行事件的相关判定
            eventChoiceBtns[i].AddListenerOnClick(
                () => { 
                    model.CurrentEventChoice = eventChoices[idx];
                    StartCoroutine(HandleEvent(model.CurrentEventChoice)); 
                }
            );
        }
    }
}
