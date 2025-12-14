using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 表明该数据类中能够获取数据的索引,用于相关数据的获取
/// </summary>
/// <typeparam name="TKey"></typeparam>
public interface IHaveKey<TKey>
{
    TKey GetKey();
}
