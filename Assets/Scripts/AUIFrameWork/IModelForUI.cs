using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 与TextUI合作,作为其Model
/// </summary>
public interface IModelForUI<T>
{
    public event Action<T> OnModelChanged;
}
