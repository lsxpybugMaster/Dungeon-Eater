using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//抽象泛型基类
/// <summary>
/// 管理持久化数据,并且自己读取并初始化数据,与GameManger分离
/// </summary>
/// <typeparam name="TData">该持久化数据容器所存储的数据类型</typeparam>
public abstract class BaseState<TData> : IDataState where TData : ScriptableObject
{
    /// <summary>
    /// 状态对应的配置数据（ScriptableObject）
    /// </summary>
    public TData BaseData { get; protected set; }

    public void LoadDataFromResources(string path)
    {
        BaseData = ResourceLoader.LoadSafe<TData>(path);
        if (BaseData == null)
        {
            Debug.LogError($"Failed to load {typeof(TData).Name} from {path}");
        }
    }
}
