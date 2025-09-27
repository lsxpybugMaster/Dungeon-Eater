using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ע��GameActionҪ�����Ӧ������System��ע��
/// </summary>
public class PlayCardGA : GameAction
{
    public EnemyView ManualTarget {  get; private set; }
    public Card Card { get; set; }

    public PlayCardGA(Card card)
    {
        Card = card;
        ManualTarget = null;
    }

    /// <summary>
    /// �ù����������������ֶ�Ŀ��Ŀ��Ƽ���
    /// </summary>
    /// <param name="card"></param>
    /// <param name="target"></param>
    public PlayCardGA(Card card, EnemyView target)
    {
        Card = card;
        ManualTarget = target;
    }
}

