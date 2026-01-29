using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//统一管理卡牌Performer
public class CardPerformSystem : IActionPerformerSystem 
{
    public void Register()
    {
        ActionSystem.AttachPerformer<HealGA>(HealGAPerformer);
    }

    public void UnRegister()
    {
        ActionSystem.DetachPerformer<HealGA>();
    }

    private IEnumerator HealGAPerformer(HealGA healGA)
    {
        //直接调用打包好的动画工具
        yield return MotionUtil.Dash(
            healGA.Caster.transform,
            new Vector2(0, 0.8f),
            Config.Instance.attackTime
        );

        //执行生命回复逻辑
        foreach (var view in healGA.Targets)
        {
            view.M.Heal(healGA.Amount);
        }
    }

   
}
