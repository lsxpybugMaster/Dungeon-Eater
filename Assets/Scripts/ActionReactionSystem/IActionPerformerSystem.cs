using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 考虑到后面这种只负责处理事件注册和实现Performer的System无需Mono挂载
/// 不如直接作为 C# 类再统一配置, 避免挂载太多到场景中 
/// </summary>
public interface IActionPerformerSystem
{
    void Register();
    void UnRegister();
}
