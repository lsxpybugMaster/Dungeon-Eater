using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystemTest
{
    public class Kard : MonoBehaviour
    {
        void OnMouseDown()
        {
            //其他动作执行时禁止操作
            if (ActionSystem.Instance.IsPerforming) return;
            DrawCardGA drawCardGA = new();

            Debug.Log("执行Perform(drawCardGA)");
            ActionSystem.Instance.Perform(drawCardGA);
            Destroy(gameObject);
        }
    }
}
