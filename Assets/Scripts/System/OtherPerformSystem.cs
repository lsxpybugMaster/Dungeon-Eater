using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("暂时无需")]
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
