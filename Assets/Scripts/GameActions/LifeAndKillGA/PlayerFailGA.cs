using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//类似KillEnemyGA
public class PlayerFailGA : GameAction
{
    public HeroView heroView { get; private set; }

    public PlayerFailGA(HeroView heroView)
    { 
        this.heroView = heroView;
    }
}
