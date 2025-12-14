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
        ActionSystem.AttachPerformer<AttackHeroGA>(AttackHeroPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
        ActionSystem.AttachPerformer<KillAllEnemyGA>(KillAllEnemyPerformer);
        ActionSystem.AttachPerformer<DecideEnemyIntendGA>(DecideEnemyIntendPerformer);
    }


    private void OnDisable()
    {     
        ActionSystem.DetachPerformer<AttackHeroGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
        ActionSystem.DetachPerformer<KillAllEnemyGA>();
        ActionSystem.DetachPerformer<DecideEnemyIntendGA>();
    }


    public void Setup(List<EnemyData> enemyDatas)
    {
        List<EnemyData> enemies = enemyDatas.GetRandomN(Random.Range(2,4));

        // foreach (var enemyData in enemyDatas)
        foreach (var enemyData in enemies)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    //执行敌人的行动
    public void DoEnemyIntend(EnemyView enemyView)
    {
        GameAction enemyAction = enemyView.EnemyAI.GetEnemyAction();
        ActionSystem.Instance.AddReaction(enemyAction);
    }


    private IEnumerator DecideEnemyIntendPerformer(DecideEnemyIntendGA decideEnemyIntendGA)
    {
        //显示敌人意图(同时计算出敌人意图)
        foreach (var enemy in Enemies)
        {
            EnemyIntend intend = enemy.EnemyAI.GetPrepareEnemyIntend();
        }
        yield return null;
    }   
    

    private IEnumerator AttackHeroPerformer(AttackHeroGA ga)
    {
        EnemyCombatant attacker = (EnemyCombatant)ga.Attacker.M;

        // 在这里分发敌人攻击(轻击, 重击等共通逻辑),目前仅仅是修改
        if (ga.SkillType == EnemySkill.FixedHit)
        {
            DealFixedAttackGA dealFixedAttackGA = new(attacker.FixedAttackPower, new() { HeroSystem.Instance.HeroView }, ga.Caster);
            ActionSystem.Instance.AddReaction(dealFixedAttackGA);
            yield break;
        }

        string diceStr = ga.SkillType == EnemySkill.LightHit ? attacker.LightAttackPowerStr : attacker.HeavyAttackPowerStr;

        DealAttackGA dealAttackGA = new(diceStr, new() { HeroSystem.Instance.HeroView }, ga.Caster, null);
        ActionSystem.Instance.AddReaction(dealAttackGA);

        yield return null;
    }


    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemyGA)
    {
        //这里有是否杀死全部敌人完成战斗的判断
        yield return enemyBoardView.RemoveEnemy(killEnemyGA.EnemyView);
    }


    /// <summary>
    /// 杀死全部敌人的反应写在这里
    /// </summary>
    /// <param name="killAllEnemyGA"></param>
    /// <returns></returns>
    private IEnumerator KillAllEnemyPerformer(KillAllEnemyGA killAllEnemyGA)
    {

        yield return new WaitForSeconds(Config.Instance.logicBetweentime);

        //同层次逻辑直接声明Reaction,不同层逻辑则注册反应进行解耦
        // 显示胜利UI
        // ShowWinUIGA showWinUIGA = new ShowWinUIGA();
        // ActionSystem.Instance.AddReaction(showWinUIGA);
    }
}
