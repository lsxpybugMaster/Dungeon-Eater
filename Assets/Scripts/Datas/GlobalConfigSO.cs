using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GlobalConfigSO")]
public class GlobalConfigSO : ScriptableObject
{
    [Header("全局信息(为 0 时代表种子随机)")]
    public int seed; //随机数种子

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
}
