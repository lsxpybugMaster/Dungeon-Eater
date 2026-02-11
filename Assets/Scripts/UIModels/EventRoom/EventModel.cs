using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 与<see cref="EventUI">配合,作为该 view 的 Model
/// 负责初始化事件房的具体事件
/// 以及注册所有相关事件
/// </summary>
public class EventModel
{
    public EventData CurrentEvent { get; set; }         //事件
    public EventChoice CurrentEventChoice { get; set; } //玩家选择的事件分支
    public bool ChoiceNeedCheck => CurrentEventChoice.PointNeed != 0;

    public EventModel()
    {
        OnInit(); //初始化时注册函数
    }

    public void GenerateEvent()
    {
        CurrentEvent = EventDataBase.GetRandomEvent();
    }
    
    //检定事件
    public void CheckChoice()
    {

    }

    public void OnInit()
    {
        ActBus.Subscribe<EEvent1>(OnEEvent1);
        ActBus.Subscribe<EEvent2>(OnEEvent2);
    }

    //需要View被销毁时手动调用
    public void OnDestroy()
    {
        ActBus.UnSubscribe<EEvent1>();
        ActBus.UnSubscribe<EEvent2>();
    }


    //示例事件1 : 无需检定的事件, 立即结束
    private IEnumerator OnEEvent1(EEvent1 e)
    {
        Debug.Log($"事件EEvent1激活 : {e.x}");
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"事件EEvent1结束");
    }


    private IEnumerator OnEEvent2(EEvent2 e)
    {
        Debug.Log($"事件EEvent2激活 : {e.y}");
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"事件EEvent2结束");
    }
}
