using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  全局事件中心
    轻量化的ActionSystem
    用于全局事件管理（不像ActionSystem管理复杂的反应链）
 
    //NOTE: 注册该事件总线需要首先定义事件类型(struct)
    public struct MyEvent
    {
        public T typename;
        public MyEvent(T t)
        {
            this.typename = t;
        }
    }
    
    //具体注册:
    EventBus.Subscribe<MyEvent>()

    //函数:
    private void OnWhat(MyEvent e){
    
    }

    实例参考:
        RoomChangedEvent
*/

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> eventTable = new();

    public static void Subscribe<T>(Action<T> listener)
    {
        if (!eventTable.ContainsKey(typeof(T)))
        {
            // 初始化null确保转换时不会出错
            eventTable[typeof(T)] = null;
        }
        eventTable[typeof(T)] = (Action<T>)eventTable[typeof(T)] + listener;
    }

    public static void UnSubscribe<T>(Action<T> listener)
    {
        if (eventTable.ContainsKey(typeof(T)))
        {
            eventTable[typeof(T)] = (Action<T>)eventTable[typeof(T)] - listener;
        }
    }

    //广播事件 evt 将参数封装 给委托进行调用
    //泛型版本,不支持多态
    public static void Publish<T>(T evt)
    {
        if (eventTable.TryGetValue(typeof(T), out var act)) 
        {
            (act as Action<T>)?.Invoke(evt);
        }
    }

    public static void ClearAll()
    {
        eventTable.Clear();
    }
}

/// <summary>
/// ActBus 和 EventBus 类似
/// EventBus 存取 Delegate
/// ActBus 存取 Func<object, IEnumrator>
/// 是真正的轻量化 ActionSystem
/// </summary>
public static class ActBus
{
    private static readonly Dictionary<Type, Func<EditableEvents, IEnumerator>> eventTable = new();

    //由于这种注册法使用了Lambda无法取消注册,所以目前仅允许注册一个事件
    public static void Subscribe<T>(Func<T, IEnumerator> listener) where T : EditableEvents
    {
        //必须额外包装一层, 因为不允许泛型逆变
        eventTable[typeof(T)] = (evt) =>
        {
            return listener((T)evt);
        };
    }

    public static void UnSubscribe<T>() where T : EditableEvents
    {
        if (eventTable.ContainsKey(typeof(T)))
        {
            eventTable[typeof(T)] = null;
        }
    }

    //ActBus 无法支持泛型, 需要运行时多态确保事件依据子类类型具体分发
    public static IEnumerator Perform(EditableEvents evt)
    {
        Type type = evt.GetType();

        if (eventTable.TryGetValue(type, out var act))
        {
            yield return act(evt);
        }
    }

    public static void ClearAll()
    {
        eventTable.Clear();
    }
}
