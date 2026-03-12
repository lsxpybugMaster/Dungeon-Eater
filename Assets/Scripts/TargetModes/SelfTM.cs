using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仅作为标记,内部逻辑翻译并解析
//IMPORTANT: 与HeroTM区分,否则玩家卡牌无效!!
public class SelfTM : TargetMode
{
    public override List<CombatantView> GetTargets(CombatantView manualTarget)
    {
        return new(){ manualTarget };
    }
}
