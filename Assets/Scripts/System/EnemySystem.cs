using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    /*
        Action注册: 
        1. 在Performer中声明逻辑
        2. 声明注册和取消注册逻辑
     */
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
    }


    private void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
    }


    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn");
        yield return new WaitForSeconds(2f);
        Debug.Log("End Enemy Turn");
    }
}
