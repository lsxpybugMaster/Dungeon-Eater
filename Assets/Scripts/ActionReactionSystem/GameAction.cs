using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    //��ActionSystem��Flow�а�˳�������ִ��

    //ִ�ж���֮ǰ��Ӧ
    public List<GameAction> PreReactions { get; private set; } = new();

    //ִ�ж���ʱ��Ӧ
    public List<GameAction> PerformReactions { get; private set; } = new();

    //ִ�ж�����Ӧ
    public List<GameAction> PostReactions { get; private set;} = new();
}
