using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/*
 *  System构建方法：
 *  1. 定义Performer函数
 *      IEnumerator PerformerName(GameAction ga)
 *  2. 在OnEnable 和 OnDisable 中注册和取消注册
 *  3. 在Effect中包装: 见AddStatusEffectEffect 
 */
public class StatusEffectsSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddStatusEffectGA>(AddStatusEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();   
    }

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA addStatusEffectGA)
    {
        foreach (var target in addStatusEffectGA.Targets)
        {
            //向上跳一小段距离
            //Tween tween = target.transform.DOMoveY(target.transform.position.y + 0.5f, 0.1f);
            //yield return tween.WaitForCompletion();
            ////退回原位
            //target.transform.DOMoveY(target.transform.position.y - 0.5f, 0.1f);

            //直接调用打包好的动画工具
            yield return MotionUtil.Dash(
                target.transform,
                new Vector2(0, 0.8f),
                Config.Instance.attackTime
            );

            target.M.AddStatusEffect(addStatusEffectGA.StatusEffectType, addStatusEffectGA.StackCount); 
            // yield return null;
        }
    }
}
