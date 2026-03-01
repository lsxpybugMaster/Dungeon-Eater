using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRewardCardGA : GameAction
{
    public int Choices { get; set; }

    public ChooseRewardCardGA(int choices)
    {
        Choices = choices;
    }
}
