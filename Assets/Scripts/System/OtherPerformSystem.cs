using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPerformSystem : MonoBehaviour
{
    private void OnEnable()
    {
        //ActionSystem.AttachPerformer<UpdateBattleInfoGA>(UpdateBattleInfoPerformer);
    }

    private void OnDisable()
    {
        //ActionSystem.DetachPerformer<UpdateBattleInfoGA>();
    }

    private IEnumerator UpdateBattleInfoPerformer(UpdateBattleInfoGA ga)
    {     
        yield return null;
    }

}
