using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class EditableEvents
{
    [field: SerializeField] public string EventResultInfo { get; set; } 

    public EditableEvents()
    {

    }
}


[Serializable]
public class EEvent1 : EditableEvents
{
    public int x = 0;
}


[Serializable]
public class EEvent2 : EditableEvents
{
    public int y = 0;
}

/// <summary>
/// 空效果事件, 可以用于没有任何副作用的触发事件上
/// </summary>
[Serializable]
public class EmptyEvent : EditableEvents
{

}
