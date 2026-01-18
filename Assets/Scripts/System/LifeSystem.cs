using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与DamageSystem紧密连接,
/// DamageSystem决定伤害,CombatantModel计算伤害并改变生命,再由LifeSystem决定生命归零后行为
/// </summary>
public class LifeSystem : MonoBehaviour
{
    //TODO: 基于生命变化的事件系统? => Damage System 重构
    void OnEnable()
    {
       
    }

    void OnDisable()
    {

    }


}
