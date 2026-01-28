using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatantData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Health { get; private set; }

    /*1d20 + 熟练项 - 目标敏捷项 == 攻击掷骰*/

    //熟练项加值
    [field: SerializeField] public int Proficiency { get; private set; }

    //敏捷项加值
    [field: SerializeField] public int Flexbility { get; private set; }
}
