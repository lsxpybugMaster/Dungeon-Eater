using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackHeroGA : GameAction, IHaveCaster
{
     public EnemyView Attacker { get; private set; }

     //来自接口
     public CombatantView Caster { get; private set; }

     public AttackHeroGA(EnemyView attacker)
     {
         Attacker = attacker;
         Caster = Attacker;
     }
}
