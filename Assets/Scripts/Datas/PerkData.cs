using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天赋数据
//核心逻辑在Perk中
[CreateAssetMenu(menuName = "Data/Perk")]
public class PerkData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }

    [Header("效果触发次数, -1为无限次")]
    [SerializeField] private int effectTimes = -1; 
    public int EffectTimes => effectTimes;

    [field: SerializeReference, SR] public PerkCondition PerkCondition { get; private set; }
    [field: SerializeReference, SR] public AutoTargetEffect AutoTargetEffect { get; private set; }
    [field: SerializeField] public bool UseAutoTarget {  get; private set; } = true;
    //将动作的触发者作为反应的目标
    [field: SerializeField] public bool UseActionCasterAsTarget { get; private set; } = false;
}
