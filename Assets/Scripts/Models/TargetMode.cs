using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TargetMode
{
    //���󷽷�,����ֵΪ���д���������Ŀ��
    public abstract List<CombatantView> GetTargets();
}
