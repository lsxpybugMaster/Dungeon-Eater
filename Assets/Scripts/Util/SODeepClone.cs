using UnityEngine;
using System;

/// <summary>
/// 通用、可复用、支持 SerializeReference 的 ScriptableObject 深拷贝工具类
/// 使用 Unity JsonUtility 生成完整独立副本，不污染原始资源。
/// </summary>
public static class SODeepCloneUtil
{
    /// <summary>
    /// 深拷贝一个 ScriptableObject，包括其内部所有字段（含 SerializeReference 多态结构）
    /// 返回的对象与原 SO 完全独立，可安全修改。
    /// </summary>
    public static T Clone<T>(T source) where T : ScriptableObject
    {
        if (source == null)
        {
            Debug.LogError("SODeepClone.Clone: source is null");
            return null;
        }

        // 1. 将 ScriptableObject 转为 JSON（支持 SerializeReference）
        string json = JsonUtility.ToJson(source, true);

        // 2. 创建新的实例
        T clone = ScriptableObject.CreateInstance<T>();

        // 3. 使用 JSON 填充 Clone，完成深度拷贝
        JsonUtility.FromJsonOverwrite(json, clone);

        return clone;
    }

    /// <summary>
    /// 深拷贝任意对象（T 是可序列化的普通类，也支持 SerializeReference）
    /// </summary>
    public static T CloneObject<T>(T source)
    {
        if (source == null) return default;

        string json = JsonUtility.ToJson(new Wrapper<T> { Value = source }, true);
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Value;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T Value;
    }
}
