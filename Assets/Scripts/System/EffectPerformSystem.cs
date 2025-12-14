using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectPerformSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnVFX;
    [SerializeField] private GameObject dizzyVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DecayEffectGA>(DecayEffectPerformer);

        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
        ActionSystem.AttachPerformer<ApplyDizzyGA>(ApplyDizzyPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DecayEffectGA>();

        ActionSystem.DetachPerformer<ApplyBurnGA>();
        ActionSystem.DetachPerformer<ApplyDizzyGA>();
    }

    private IEnumerator DecayEffectPerformer(DecayEffectGA decayEffectGA)
    {
        CombatantView target = decayEffectGA.Target;
        var type = decayEffectGA.Type;
        int decay = decayEffectGA.Decay;
        target.M.RemoveStatusEffect(type, decay);
        yield return null;
    }

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        Instantiate(burnVFX, target.transform.position, Quaternion.identity);
        target.M.Damage(applyBurnGA.BurnDamage);
        
        //结算状态
        target.M.RemoveStatusEffect(StatusEffectType.BURN, 1);
        
        yield return new WaitForSeconds(Config.Instance.effectTime);
    }

    private IEnumerator ApplyDizzyPerformer(ApplyDizzyGA applyDizzyGA)
    {
        CombatantView target = applyDizzyGA.Target;
        Instantiate(dizzyVFX, target.transform.position, Quaternion.identity);

        if (target is not EnemyView)
        {
            Debug.LogWarning("玩家无法结算Sleep状态");
            yield break;
        }

        //更换Intend
        NoIntend noIntend = new();
        var enemyTarget = target as EnemyView;
        enemyTarget.EnemyAI.ChangeEnemyIntend(noIntend);

        //结算状态
        target.M.RemoveStatusEffect(StatusEffectType.DIZZY, 1);

        yield return new WaitForSeconds(Config.Instance.effectTime);
    }
}
