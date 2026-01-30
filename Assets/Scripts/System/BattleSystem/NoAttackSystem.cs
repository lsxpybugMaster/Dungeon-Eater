using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoAttackSystem : IActionPerformerSystem
{
    public void Register()
    {
        ActionSystem.AttachPerformer<UpdateAlContextGA>(UpdateAIContextPerformer);
    }

    public void UnRegister()
    {
        ActionSystem.DetachPerformer<UpdateAlContextGA>();
    }

    private IEnumerator UpdateAIContextPerformer(UpdateAlContextGA ga)
    {
        //TODO: 如何将这些耦合的动画剔除？

        yield return MotionUtil.Dash(
            ga.EnemyView.transform,
            new Vector2(0, 0.8f),
            Config.Instance.attackTime
        );

        ga.EnemyView.EnemyAI.aiContext.Set(ga.WhichContext, ga.ChangeNumber);
    }
}
