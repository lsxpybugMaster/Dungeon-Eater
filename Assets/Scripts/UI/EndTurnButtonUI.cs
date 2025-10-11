using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButtonUI : MonoBehaviour
{
    public void OnClick()
    {
        if (!UISystem.Instance.CanInteract()) return;

        Debug.Log("CLICK!");
        EnemyTurnGA enemyTurnGA = new();
        //根据传入的Action类型执行对应Action
        //注意若要执行需要先注册
        ActionSystem.Instance.Perform(enemyTurnGA);
    }
}
