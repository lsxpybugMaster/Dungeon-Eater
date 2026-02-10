using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件方事件数据库：
/// 负责管理所有 EventData 的查找、缓存与随机获取。
/// 对外提供只读接口，禁止外部直接修改或访问底层列表。
/// </summary>
[CreateAssetMenu(menuName = "DataBase/EventDatabase")]
public class EventDataBase : DataBase<string, EventData>
{
    // --------------------- 单例访问 ---------------------
    // 这块逻辑提取成基类需要泛型,Resources.Load使用泛型会有问题
    private static EventDataBase _instance;
    public static EventDataBase Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EventDataBase>("DB/EventDatabase");
                if (_instance == null)
                    Debug.LogError("CardDatabase.asset not found in Resources folder!");
                else
                    _instance.Init();
            }
            return _instance;
        }
    }

    // --------------------- 随机接口 ---------------------

    /// <summary> 随机抽取一张卡牌 </summary>
    /// 传入的函数形式 bool func(CardData)
    public static EventData GetRandomEvent()
    {
        //确定随机的范围
        int index = UnityEngine.Random.Range(0, Instance.datas.Count);
        return Instance.datas[index];
    }

}
