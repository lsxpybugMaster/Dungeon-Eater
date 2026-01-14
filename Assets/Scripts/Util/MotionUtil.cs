using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事先封装一些dotween动画,用于在Performer中演示 (非UI)
/// </summary>
public static class MotionUtil 
{
    /// <summary>
    /// 角色向某方向进行 “往返攻击位移” 的通用动画。
    /// </summary>
    /// <param name="trans">对象 Transform</param>
    /// <param name="offset">相对位移量</param>
    /// <param name="time">往返每段花费的时间</param>
    public static IEnumerator Dash(Transform trans, Vector2 offset, float time)
    {
        Vector3 originalPos = trans.position;
        Vector3 forwardPos = originalPos + (Vector3)offset;

        // 往前
        Tween t1 = trans.DOMove(forwardPos, time);
        yield return t1.WaitForCompletion();

        // 往回
        Tween t2 = trans.DOMove(originalPos, time);
        yield return t2.WaitForCompletion();
    }

    /// <summary>
    /// Dash → Slow Motion → Hit → Back
    /// </summary>
    public static IEnumerator HeavyStrike(
        Transform trans,
        Vector2 offset,
        float dashTime,
        float slowMotionScale = 0.2f,
        float slowMotionDuration = 0.08f
    )
    {
        Vector3 originalPos = trans.position;
        Vector3 forwardPos = originalPos + (Vector3)offset;

        // Step 1: 正常速度的快速冲刺
        Tween dashTween = trans.DOMove(forwardPos, dashTime)
                               .SetEase(Ease.OutQuad);
        yield return dashTween.WaitForCompletion();

        // Step 2: 瞬间慢动作（类似杀戮尖塔的击中感）
        float originalTimeScale = Time.timeScale;
        Time.timeScale = slowMotionScale;

        // 让慢动作持续 *真实时间*，而不是受到 timeScale 影响
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Step 3: 恢复正常时间
        Time.timeScale = originalTimeScale;

        // Step 4: 后撤回原位
        Tween backTween = trans.DOMove(originalPos, dashTime * 1.1f)
                               .SetEase(Ease.InOutQuad);
        yield return backTween.WaitForCompletion();
    }
}
