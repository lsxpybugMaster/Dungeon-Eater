using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理与Effect有关的Performer注册相关
/// </summary>
public class EffectSystem : IActionPerformerSystem
{
    public void Register()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    public void UnRegister()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }

    //在CardSystem.PlayCardPerformer中获取的Target
    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        // 删除空元素(说明目标已经死亡)
        // 但是注意有Targets == null 的无目标 GA
        if (performEffectGA.Targets != null)
        {
            performEffectGA.Targets.RemoveAll(t => t == null);
            if (performEffectGA.Targets.Count == 0)
                yield break;
        }

        //IMPORTANT: 一定要区分,这里是调用链上一步的结果,如果为第一个,这里为true
        if (performEffectGA.Context.MainEffectSuccess == false)
        {
            yield break;
        }

        //BUG: 这里一定要传入Context,否则后续出现空指针,因为Context传递链断掉了
        GameAction effectAction = performEffectGA.Effect.GetGameAction(performEffectGA.Targets, HeroSystem.Instance.HeroView, performEffectGA.Context );
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }

  
}
