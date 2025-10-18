using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DebugUtil
{
    // 常用颜色快捷方式
    public static void Red(string message) => Debug.Log($"<color=red>{message}</color>");
    public static void Green(string message) => Debug.Log($"<color=green>{message}</color>");
    public static void Blue(string message) => Debug.Log($"<color=blue>{message}</color>");
    public static void Yellow(string message) => Debug.Log($"<color=yellow>{message}</color>");
    public static void Orange(string message) => Debug.Log($"<color=orange>{message}</color>");
    public static void Cyan(string message) => Debug.Log($"<color=cyan>{message}</color>");
    public static void Magenta(string message) => Debug.Log($"<color=magenta>{message}</color>");

    // 带标签的日志
    public static void LogWithTag(string tag, string message, Color tagColor)
    {
        string hexColor = ColorUtility.ToHtmlStringRGB(tagColor);
        Debug.Log($"[<color=#{hexColor}>{tag}</color>] {message}");
    }
}
