using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ImmediateData : ScriptableObject
{
    /// <summary>
    /// 后面的逻辑需要在OnPickup里实现, 以便在拾取时触发效果
    /// </summary>
    public abstract void OnPickup();
}
