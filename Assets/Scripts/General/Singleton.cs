using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例基类，确保场景中同一类型只有一个实例
/// 使用方式：让你的类继承 Singleton<MyClass>
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // 静态实例，外部通过 Singleton<T>.Instance 访问
    public static T Instance { get; private set; }

    /// <summary>
    /// Unity 生命周期函数 Awake
    /// 用于初始化单例
    /// </summary>
    protected virtual void Awake()
    {
        // 如果已经有实例存在，销毁当前重复对象，保证单例唯一
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // 将当前对象赋值为唯一实例
        Instance = this as T;
    }

    /// <summary>
    /// 当应用退出时执行
    /// 释放单例引用，避免退出时残留对象
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        Instance = null;           // 清空静态实例引用
        Destroy(gameObject);       // 销毁对象（这里不是必须的，但可以确保干净退出）
    }
}


/// <summary>
/// 跨场景持久化的单例
/// 继承自 Singleton<T>，在 Awake 时调用 DontDestroyOnLoad
/// 适合用于音频管理器、数据管理器等需要跨场景保存的对象
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    /// <summary>
    /// Awake 被重写：在基类 Awake 设置单例后，再调用 DontDestroyOnLoad
    /// </summary>
    protected override void Awake()
    {
        base.Awake();                    // 先执行 Singleton<T>.Awake() 确认单例
        DontDestroyOnLoad(gameObject);   // 防止该对象在切换场景时被销毁
    }
}
