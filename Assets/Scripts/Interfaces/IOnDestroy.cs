using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 含有该接口的非Mono需要手动管理销毁
/// </summary>
public interface IOnDestroy
{
    public void OnDestroy();
}
