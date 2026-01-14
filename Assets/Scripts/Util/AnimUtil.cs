using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 封装一些播放dotween的辅助函数
/// </summary>
public static class AnimUtil
{
    /// <summary>
    /// 从Unity LayoutGroup中分离,使得我们可以播放后续的位置相关动画
    /// </summary>
    /// <param name="rect"></param>
    public static void DetachFromLayoutGroup(RectTransform rect, RectTransform parentRect)
    {
        //保存世界位置
        Vector3 worldPosition = rect.position;
      
        rect.SetParent(parentRect, false);

        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
       
        rect.position = worldPosition;
    }
}
