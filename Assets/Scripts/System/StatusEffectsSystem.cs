using System.Collections;
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
        ActionSystem.AttachPerformer<AddRandomStatusEffectGA>(AddRandomStatusEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddStatusEffectGA>();   
        ActionSystem.DetachPerformer<AddRandomStatusEffectGA>();
    }

    private IEnumerator AddRandomStatusEffectPerformer(AddRandomStatusEffectGA ga)
    {
        //处理随机部分
        int result = CheckUtil.Throw(ga.StackCountStr, "AddStack");
        //更新数值,之后就可以分析其StackCount属性了
        ga.SetStackCount(result);

        yield return AddStatusEffect(ga);
    }

    private IEnumerator AddStatusEffectPerformer(AddStatusEffectGA ga)
    {
        yield return AddStatusEffect(ga);
    }

    //由于固定的和随机的都要用, 所以提取出公共部分便于修改
    private IEnumerator AddStatusEffect(AddStatusEffectGA ga)
    {
        foreach (var target in ga.Targets)
        {

            //TODO: 如何将这些耦合的动画剔除？
            //直接调用打包好的动画工具
            yield return MotionUtil.Dash(
                target.transform,
                new Vector2(0, 0.8f),
                Config.Instance.attackTime
            );

            target.M.AddStatusEffect(ga.StatusEffectType, ga.StackCount);
            // yield return null;
        }
    }
}
