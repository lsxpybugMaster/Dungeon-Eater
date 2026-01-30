using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 考虑到后续 System 过多,我们提取出一些 IActionPerformerSystem
/// 作为依赖注入到对应的系统中, 而不是使用 Mono 控制
/// 注意还是有一些真正的 System 要作为 Mono 挂载在场景中, 执行需要 Mono 的功能
/// </summary>
public class ActionsRegister : MonoBehaviour
{
    private List<IActionPerformerSystem> systems;

    void Awake()
    {
        //在这里进行依赖注入, 而不是在场景中挂载, 让真正有意义的 System 在场景中发挥作用
        systems = new()
        {
            new AttackSystem(),
            new NoAttackSystem(),
            new EffectSystem(),
            new StatusEffectsSystem(),
            new CardPerformSystem(),
            new GameProgressSystem(),
        };

        foreach (var sys in systems)
            sys.Register();
    }

    void OnDestroy()
    {
        foreach (var sys in systems)
            sys.UnRegister();
    }
}
