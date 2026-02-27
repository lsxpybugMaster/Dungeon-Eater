using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk : IFood
{
    public Sprite Image => data.Image;

    private readonly PerkData data;

    private readonly PerkCondition condition;

    private readonly AutoTargetEffect effect;

    //会被修改的相关信息
    public int Effectimes { get; private set; }

    public Perk(PerkData perkData)
    {
        data = perkData;
        condition = data.PerkCondition;
        effect = data.AutoTargetEffect;
        
        Effectimes = data.EffectTimes;
    }

    public void OnAdd()
    {
        condition.SubscribeCondition(Reaction);
    }

    public void OnMove()
    {
        condition.UnsubscribeCondition(Reaction);
    }

    private void Reaction(GameAction gameAction)
    {
        //只有有触发次数的天赋,该变量才会变为0, 之后不再触发
        if (Effectimes == 0)
        {
            return;
        }

        if (condition.SubConditionIsMet(gameAction))
        {
            List<CombatantView> targets = new();
            if (data.UseActionCasterAsTarget && gameAction is IHaveCaster haveCaster)
            {
                targets.Add(haveCaster.Caster);
            }
            if (data.UseAutoTarget)
            {
                targets.AddRange(effect.TargetMode.GetTargets(null));
            }
            //动作的执行者为敌人
            GameAction perkEffectAction = effect.Effect.GetGameAction(targets, HeroSystem.Instance.HeroView, null);

            ActionSystem.Instance.AddReaction(perkEffectAction);

            Effectimes--; //每次触发后, 触发次数-1 
        }
    }

    public void OnPickup()
    {
        
    }

    public void OnReMove()
    {
      
    }
}
