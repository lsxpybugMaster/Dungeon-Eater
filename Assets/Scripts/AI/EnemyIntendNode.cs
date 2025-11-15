using SerializeReferenceEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 后续可以作为行为树节点,包含了条件 + 概率 + 具体行为
/// </summary>
[Serializable]
public class EnemyIntendNode
{
    [SerializeReference, SR]
    public IntendCondition Condition;

    [SerializeReference, SR]
    public EnemyIntend Intend;

    /// <summary>
    /// 触发概率
    /// </summary>
    [Range(0f, 1f)]
    public float P;

}
