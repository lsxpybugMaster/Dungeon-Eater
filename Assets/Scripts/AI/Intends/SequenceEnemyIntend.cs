using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceEnemyIntend : EnemyIntend
{
    [SerializeReference, SR]
    public EnemyIntend[] enemyIntendSeq;

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
