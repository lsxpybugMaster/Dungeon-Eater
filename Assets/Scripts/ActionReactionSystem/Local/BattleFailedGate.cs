using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFailedGate : IActionExecutionGate
{
    public bool CanStart(GameAction action)
    {
        if (GameManager.Instance.GameState == GameState.Fail)
        {
            return false;
        }
            //后续可以加一个放行项
            // return action.AllowWhenFail;

        return true;
    }

    public bool CanContinue(GameAction action)
    {      
        return GameManager.Instance.GameState != GameState.Fail;
            //|| action.AllowWhenFail;
    }

}
