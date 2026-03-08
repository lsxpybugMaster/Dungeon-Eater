using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceEnemyIntend : EnemyIntend, IHaveDmgInfo
{
    [SerializeReference, SR]
    public EnemyIntend[] enemyIntendSeq;


    //为了能够显示Intend信息,需要解析其是否有伤害信息
    string dmgInfo = "";
    public string GetDmgInfo(EnemyView enemyView)
    {
        
        for (int i = 0; i < enemyIntendSeq.Length; i++)
        {
            EnemyIntend intend = enemyIntendSeq[i];
            if (intend is IHaveDmgInfo info)
            {
                dmgInfo = info.GetDmgInfo(enemyView);
            }
            else if (intend is AttackIntend atk)
            {
                dmgInfo = atk.GetDmgStr;
            }
        }
        return dmgInfo;
    }

    public override GameAction GetGameAction(EnemyView enemy)
    {
        SeqenceGameAction seqGA = new();
        for (int i = 0; i < enemyIntendSeq.Length; i++)
        {
            seqGA.Add(enemyIntendSeq[i].GetGameAction(enemy));
        }
        return seqGA;
    }
}
