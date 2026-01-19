using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//把是否允许执行抽象成接口,这样我们可以将外部控制注入(如玩家失败时禁用随后的所有逻辑)
//使用时只需声明一个含该接口的类并实现判断函数再注入即可
public interface IActionExecutionGate
{
    /// <summary>
    /// 是否允许开始执行一个 Action
    /// </summary>
    bool CanStart(GameAction action);

    /// <summary>
    /// 是否允许继续执行当前 Flow
    /// </summary>
    bool CanContinue(GameAction action);
}
