using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: MapDiceView(V) MapDice(M) MapDicesSystem (IoC)
public class MapDice 
{
    //TODO: 目前只存取Index,之后可能存取其他反应
    public int Index {get; set;}
    //仅负责记录最初的位置便于初始化,后续不更新! 因为有Index就能推出真正位置。
    public Vector3 start_pos { get; set;}
}
