using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    //管理游戏中敌人对象
    [SerializeField] private EnemyBoardView enemyBoardView;

    //提供敌人列表供其他模块调用
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

    /*
        Action注册: 
        1. 在Performer中声明逻辑
        2. 声明注册和取消注册逻辑
     */
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
        ActionSystem.AttachPerformer<KillAllEnemyGA>(KillAllEnemyPerformer);
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
        ActionSystem.DetachPerformer<KillAllEnemyGA>();
    }


    public void Setup(List<EnemyData> enemyDatas)
    {
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }


    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        //到了敌人的回合,遍历每个敌人并执行逻辑
        foreach (var enemy in enemyBoardView.EnemyViews)
        {
            //结算燃烧状态
            int burnStacks = enemy.GetStatusEffectStacks(StatusEffectType.BURN);
            if (burnStacks > 0)
            {
                ApplyBurnGA applyBurnGA = new(burnStacks, enemy);
                ActionSystem.Instance.AddReaction(applyBurnGA);
            }
            
            AttackHeroGA attackHeroGA = new(enemy);
            ActionSystem.Instance.AddReaction(attackHeroGA);
        }
        yield return null;
    }

    private IEnumerator AttackHeroPerformer(AttackHeroGA attackHeroGA)
    {
        EnemyView attacker = attackHeroGA.Attacker;
        //向前进一小段距离
        Tween tween = attacker.transform.DOMoveX(attacker.transform.position.x - 1f, 0.15f);
        yield return tween.WaitForCompletion();
        //退回原位
        attacker.transform.DOMoveX(attacker.transform.position.x + 1f, 0.15f);

        //造成伤害作为公共功能,应当封装为GameAction
        //new(){ HeroSystem.Instance.HeroView } 创建了一个List,初始化元素为HeroSystem.Instance.HeroView
        DealDamageGA dealDamageGA = new(attacker.AttackPower, new(){ HeroSystem.Instance.HeroView }, attackHeroGA.Caster);

        ActionSystem.Instance.AddReaction(dealDamageGA);
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }


    /// <summary>
    /// 杀死全部敌人的反应写在这里
    /// </summary>
    /// <param name="killAllEnemyGA"></param>
    /// <returns></returns>
    private IEnumerator KillAllEnemyPerformer(KillAllEnemyGA killAllEnemyGA)
    {
        Debug.Log("PLAYER WIN!!");

        yield return new WaitForSeconds(1);

        //同层次逻辑直接声明Reaction,不同层逻辑则注册反应进行解耦
        //继续执行
        ShowWinUIGA showWinUIGA = new ShowWinUIGA();
        ActionSystem.Instance.AddReaction(showWinUIGA);
    }
}
