using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

/*
    Trait 实现:
    接口 + 扩展方法
 */
public interface IRequireGameManager { }

public static class RequireGameManagerTrait
{
    public static IEnumerator WaitGameManagerReady(this IRequireGameManager owner, Action setupFunction)
    {
        //NOTE: 参数的类型约束是为了限制,实际使用时仍需当作Mono分析
        var mb = owner as MonoBehaviour;
        if (mb == null)
            yield break;

        while (GameManager.Instance == null)
            yield return null;

        while (GameManager.Instance.Phase != GameManagerPhase.Ready)
            yield return null;

        setupFunction.Invoke();        
    }
}