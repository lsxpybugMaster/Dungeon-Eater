using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对于具有动态伤害(如多次攻击)的内容,实现该接口
/// </summary>
public interface IHaveDmgInfo
{
    string GetDmgInfo(EnemyView enemyView);
}