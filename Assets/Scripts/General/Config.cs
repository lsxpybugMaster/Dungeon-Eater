using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 全局配置,不再在每个系统中配置数据,这样不便于管理
/// </summary>
public class Config : PersistentSingleton<Config>
{
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

    [Header("战斗动画相关参数")]
    public float attackTime;

    [Header("逻辑显示相关参数")]
    public float logicBetweentime; //如击败全部敌人后显示胜利的时间间隔

}
