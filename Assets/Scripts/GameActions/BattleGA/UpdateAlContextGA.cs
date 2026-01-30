using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateAlContextGA : GameAction
{
    public AI WhichContext { get; set; }

    public int ChangeNumber { get; set; } //改变多少

    public EnemyView EnemyView { get; set; }
    

    public UpdateAlContextGA(EnemyView enemyView, AI whichContext, int changeNumber)
    {
        EnemyView = enemyView;
        WhichContext = whichContext;
        ChangeNumber = changeNumber;
    }
}
