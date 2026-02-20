using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ImmediateData/SO_GetMoney")]
public class SO_GetMoney : ImmediateData
{
    public override void OnPickup()
    {
       Debug.Log("道具属性执行: 获得金钱");
    }
}
