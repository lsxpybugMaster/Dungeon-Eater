using SerializeReferenceEditor;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EventChoice
{
    [Header("展示在按钮上的信息")]
    [SerializeField] private string choiceInfo;
    public string ChoiceInfo { get => choiceInfo; set => choiceInfo = value; }

    [Header("检定所需点数(为 0 代表无需检定)")]
    [SerializeField] private int pointNeed;
    public int PointNeed { get => pointNeed; set => pointNeed = value; }

    //外部配置事件参数类, 需要使用插件扩展Unity对多态的支持
    [field: SerializeReference, SR] public EditableEvents ClickEvent { get; private set; } = null;    
}


[CreateAssetMenu(menuName = "Data/Event")]
public class EventData : ScriptableObject, IHaveKey<string>
{
    [field: SerializeField] public string EventName { get; set; }
    [field: SerializeField] public string EventDescription { get; set; }

    //依据选择个数初始化按钮个数
    [field: SerializeField] public List<EventChoice> EventChoice { get; set; }

    public string GetKey() => EventName;
}
