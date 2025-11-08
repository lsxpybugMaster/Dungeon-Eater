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
    public static void Publish<T>(T evt)
    {
        if (eventTable.TryGetValue(typeof(T), out var act)) 
        {
            (act as Action<T>)?.Invoke(evt);
        }
    }
}
