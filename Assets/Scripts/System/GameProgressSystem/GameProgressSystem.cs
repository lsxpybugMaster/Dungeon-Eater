using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与游戏流程有关的相关事件注册
/// </summary>
public class GameProgressSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PlayerFailGA>(PlayerFail);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<PlayerFailGA>();
    }

    //游戏结束相关准备,首先执行玩家失败的相关逻辑
    //NOTE: 如果后期玩家会死在非战斗场景,需要修改
    private IEnumerator PlayerFail(PlayerFailGA playerFailGA)
    {
        Debug.Log("执行PlayerFail");
        GameManager.Instance.SceneModeManager.Fail();
        //之后GameAction会都失效

        //玩家死亡的消失动画
        HeroView view = playerFailGA.heroView;
        Tween tween = view.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        //对应的UI显示
        UISystem.Instance.ShowFailUI();
        


        //锁定逻辑
    }
}
