using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 目前提供一些辅助字符串处理函数
/// </summary>
public static class StringExtensions
{
    // 指定颜色
    public static string ColorTo(this string str, Color color)
    {
        string hexColor = ColorUtility.ToHtmlStringRGBA(color);
        return $"<color=#{hexColor}>{str}</color>";
    }

    // 使用颜色名称或十六进制
    public static string ColorTo(this string str, string colorCode)
    {
        return $"<color={colorCode}>{str}</color>";
    }

}
