using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEffectGA : GameAction
{
    public CombatantView CombatantView { get; set; }

    public UpdateEffectGA(CombatantView combatantView) 
    {
        CombatantView = combatantView;
    }

}
