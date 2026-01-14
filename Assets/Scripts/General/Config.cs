using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局配置,不再在每个系统中配置数据,这样不便于管理
/// </summary>
public class Config : PersistentSingleton<Config>
{
    [SerializeField] private GlobalConfigSO so;

    private new void Awake()
    {
        base.Awake();

        CopyData(so);
    }

    private void CopyData(GlobalConfigSO so)
    {
        seed = so.seed == 0 ? Random.Range(0, int.MaxValue) : seed;
        difficultScore = so.difficultScore;
        cardSize = so.cardSize;
        cardShowSize = so.cardShowSize;
        freezeTime = so.freezeTime;
        moveTime = so.moveTime;
        scaleTime = so.scaleTime;
        effectTime = so.effectTime;
        attackTime = so.attackTime;
        showCardTime = so.showCardTime;
        logicBetweentime = so.logicBetweentime;  //如击败全部敌人后显示胜利的时间间隔
    }

    private int seed; //随机数种子
    public int Seed { get { return seed; } }    

    [Header("战斗数值相关系统")]
    public int difficultScore;

    [Header("卡牌系统相关参数")]
    //卡牌大小
    [Range(0, 2f)] public float cardSize;
    //卡牌作为动画展示时的大小
    [Range(0, 2f)] public float cardShowSize;

    [Header("卡牌动画相关参数")]
    //展示卡牌对象的时间
    public float freezeTime;
    //移动卡牌对象动画的时间
    public float moveTime;
    //卡牌缩放/膨胀所需的时间
    public float scaleTime;

    [Header("效果系统相关参数")]
    //显示VFX的时间
    public float effectTime;

    [Header("战斗动画相关参数")]
    public float attackTime;

    [Header("UI动画相关参数")]
    public float showCardTime;

    [Header("逻辑显示相关参数")]
    public float logicBetweentime; //如击败全部敌人后显示胜利的时间间隔





    //[Header("全局信息")]
    //[SerializeField] private int seed; //随机数种子
    //public int Seed 
    //{
    //    get
    //    {
    //        Debug.Log($"seed = {seed}");
    //        return seed == 0 ? UnityEngine.Random.Range(0, int.MaxValue) : seed;
    //    }
    //}

    //[Header("战斗数值相关系统")]
    //public int difficultScore;

    //[Header("卡牌系统相关参数")]
    ////卡牌大小
    //[Range(0, 2f)] public float cardSize; 
    ////卡牌作为动画展示时的大小
    //[Range(0, 2f)] public float cardShowSize;

    //[Header("卡牌动画相关参数")]
    ////展示卡牌对象的时间
    //public float freezeTime;
    ////移动卡牌对象动画的时间
    //public float moveTime;
    ////卡牌缩放/膨胀所需的时间
    //public float scaleTime;

    //[Header("效果系统相关参数")]
    ////显示VFX的时间
    //public float effectTime;

    //[Header("战斗动画相关参数")]
    //public float attackTime;

    //[Header("UI动画相关参数")]
    //public float showCardTime;

    //[Header("逻辑显示相关参数")]
    //public float logicBetweentime; //如击败全部敌人后显示胜利的时间间隔

}
