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
    Start,
    Boss
}

public class MapGrid
{
    public int gridIndex;
    public GridType gridType;
}
