using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DotweenExtensions 
{
    /// <summary>
    /// 在tween完成时销毁目标对象, 适用于一些动画结束后需要销毁的对象, 如卡牌升级时的旧卡牌对象
    /// </summary>
    /// <param name="tween"></param>
    /// <param name="target"></param>
    public static void DestroyOnComplete(this Tween tween, GameObject target)
    {
        tween.OnComplete(() => {
            if (target != null)
            {
                GameObject.Destroy(target);
            }
        });
    }
}
