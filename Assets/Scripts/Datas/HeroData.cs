﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hero")]
public class HeroData : ScriptableObject
{
    [field: SerializeField] public Sprite Image {  get; private set; }
    [field: SerializeField] public int Health { get; private set; }
    //给每个英雄其自己的卡组
    [field: SerializeField] public List<CardData> Deck {  get; private set; }
}
