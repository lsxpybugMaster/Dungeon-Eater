using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仅负责处理燃烧效果,添加燃烧效果使用AddStatusEffectGA
public class ApplyBurnGA : GameAction
{
    public int BurnDamage { get; private set; }
    public CombatantView Target {  get; private set; }

    public ApplyBurnGA(int burnDamage, CombatantView target)
    {
        BurnDamage = burnDamage;
        Target = target;
    }
}
