using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    由于使用Resourse文件夹管理数据
    TDataState = Resources.Load<TDataState>("Folder/file");
    该方法不进行错误路径判断
    所以需要包装一下
 */
public static class ResourceLoader
{
    /// <summary>
    /// 从 Resources 文件夹加载资源，若失败则打印报错信息并返回 null。
    /// </summary>
    /// <typeparam name="T">资源类型（例如 HeroData）</typeparam>
    /// <param name="path">相对于 Resources 根目录的路径</param>
    /// <param name="context">调用来源，可选，用于报错定位</param>
    /// <returns>加载到的资源或 null</returns>
    public static T LoadSafe<T>(string path, Object context = null) where T : Object
    {
        T asset = Resources.Load<T>(path);

        if (asset == null)
        {
            string msg = $"Resource not found at path: \"{path}\" (type: {typeof(T).Name})";
#if UNITY_EDITOR
            // 在编辑器中高亮报错位置（点击可定位）
            Debug.LogError(msg, context);
#else
            Debug.LogError(msg);
#endif
        }

        return asset;
    }
}
