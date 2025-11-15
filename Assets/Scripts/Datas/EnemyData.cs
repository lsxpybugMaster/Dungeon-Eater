using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//参考CardData的设计
[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    // 基本数据
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    [field: SerializeField] public int AttackPower { get; private set; }

    // AI逻辑数据
    [field: SerializeReference, SR]
    public List<EnemyIntend> IntendTable { get; private set; }


}
