using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immediate : IFood
{
    private readonly ImmediateData data;

    public void OnPickup()
    {
        data.OnPickup(); //调用多态方法
    }

    public void OnReMove()
    {
        
    }

    public Immediate(ImmediateData data)
    {
        this.data = data;
    }
}
