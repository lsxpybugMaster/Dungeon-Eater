using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枚举格子类型
/// </summary>
public enum GridType
{
    Enemy,
    Shop,
    Event,
    Rest,
    Box,
    Elite,
    Boss
}

public class MapGrid
{
    public int gridIndex;
    public GridType gridType;
    public char nextDirection; //指向下一个方向

    //如果需要,存取敌人
    public List<EnemyData> roomEnemies;
}
