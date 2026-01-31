using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//静态类,对 AnimUtil / MotionUtil的终极封装 
//避免污染工具类
public static class AnimStatic
{
    /// <summary>
    /// 使用方法: yield return JumpAnim
    /// </summary>
    /// <returns></returns>
    public static IEnumerator JumpAnim(CombatantView view)
    {
        yield return MotionUtil.Dash(
            view.transform,
            new Vector2(0, 0.8f),
            Config.Instance.attackTime
        );
    }

    /// <summary>
    /// 空参代表对玩家操作
    /// </summary>
    /// <returns></returns>
    public static IEnumerator JumpAnim()
    {
        CombatantView view = HeroSystem.Instance.HeroView;
        yield return JumpAnim(view);
    }


}
