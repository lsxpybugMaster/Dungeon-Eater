using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnSystem : MonoBehaviour
{
    [SerializeField] private GameObject burnVFX;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<ApplyBurnGA>(ApplyBurnPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<ApplyBurnGA>();
    }

    private IEnumerator ApplyBurnPerformer(ApplyBurnGA applyBurnGA)
    {
        CombatantView target = applyBurnGA.Target;
        Instantiate(burnVFX, target.transform.position, Quaternion.identity);
        target.M.Damage(applyBurnGA.BurnDamage);
        
        //OPTIMIZE: 考虑到敌人不一定真的是因为状态受到燃烧伤害,把这部分逻辑交给外部处理
        //target.M.RemoveStatusEffect(StatusEffectType.BURN, 1);
        
        yield return new WaitForSeconds(1f);
    }
}
