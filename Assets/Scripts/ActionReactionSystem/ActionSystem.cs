using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ActionSystem与其他System密切相连,其注册都是在这些Systen中完成的
/// </summary>
public class ActionSystem : Singleton<ActionSystem>
{
    //反应队列,确保反应有序执行(只是一个临时指针)
    private List<GameAction> reactions = null;
    public bool IsPerforming { get; private set; } = false;


    // 【注意】 static 字段和类本身关联，不跟随某个实例的生命周期。 即这些字段不会随着场景更改而消失!
    /*
     * 订阅字典,任何类型的GameAction作为键值, 索引其对应的Action列表
     * Action接受GameAction参数不返回值
     * 实际执行GameAction时,查阅订阅字典
     */
    //执行前(瞬间执行: 鸣炮) 允许多个监听者反应
    private static Dictionary<Type, List<Action<GameAction>>> preSubs = new();
    //执行后(瞬间执行: 鞠躬) 允许多个监听者反应
    private static Dictionary<Type, List<Action<GameAction>>> postSubs = new();

    /*
     * 订阅字典
     * Func接受GameAction参数并返回IEnumerator, IEnumerator定义动作执行逻辑
     */
    //具体执行(长期执行: 演讲) 仅允许一种执行方式
    private static Dictionary<Type, Func<GameAction, IEnumerator>> performers = new();


    //存储包装器,确保能够正确删除
    private static Dictionary<Delegate, Action<GameAction>> reactionMap = new();

    #region 支持多GA执行
    //初始化AS系统时,注册一个扩展GA,其支持跳过Flow的isPerforming锁,将多个GA依序打包执行
    private void OnEnable()
    {
        AttachPerformer<PerformAllGA>(PerformAllPerformer);
        AttachPerformer<EmptyGA>(PerformEmptyGAPerformer);
    }

    private void OnDisable()
    {
        DetachPerformer<PerformAllGA>();
        DetachPerformer<EmptyGA>();
    }

    //注意可能导致的Perform冲突问题
    private IEnumerator PerformAllPerformer(PerformAllGA seqGA)
    {
        foreach (var ga in seqGA.GetSequence())
        {
            bool done = false;

            StartCoroutine(Flow(ga, () => done = true));

            while (!done)
                yield return null;
        }
    }

    private IEnumerator PerformEmptyGAPerformer(EmptyGA ga)
    {
        yield return null;
    }

    #endregion


    // NOTE 谨慎应用static变量,它们的生命周期需要手动管理
    private void OnDestroy()
    {
        preSubs.Clear();
        postSubs.Clear();
        performers.Clear();
    }

    /// <summary>
    /// 执行一个游戏动作的公开入口方法。外部代码通过调用此方法来触发一个动作。
    /// </summary>
    /// <param name="action">要执行的 GameAction 对象</param>
    /// <param name="OnPerformFinished">可选的回调，当整个动作及其所有反应完全执行完毕后触发</param>
    public void Perform(GameAction action, Action OnPerformFinished = null)
    {
        // 检查系统是否正在执行其他动作，防止动作重叠执行
        if (IsPerforming) return;

        // 设置标志位，表明系统开始忙碌
        IsPerforming = true;

        // 启动协程来处理复杂的动作流程。协程可以处理等待和序列化执行。
        // 使用 lambda 表达式创建一个回调，该回调会在 Flow 协程结束时：
        // 1. 重置 IsPerforming 标志
        // 2. 触发外部传入的 OnPerformFinished 回调
        StartCoroutine(Flow(action, () =>
        {
            IsPerforming = false;
            OnPerformFinished?.Invoke(); // ?. 是空条件操作符，如果回调为null则不执行
        }));
    }



    /// <summary>
    /// 添加反应到临时反应队列中
    /// </summary>
    /// <param name="gameAction">反应类型</param>

    public void AddReaction(GameAction gameAction)
    {
        reactions?.Add(gameAction);
    }



    /// <summary>
    /// 核心协程，定义了单个 GameAction 的完整执行流程（Pre -> Perform -> Post）
    /// 此方法像一个状态机，按顺序推进动作的各个阶段。
    /// </summary>
    /// <param name="action">要执行的动作</param>
    /// <param name="OnFlowFinished">当此特定动作的流程结束时回调</param>
    /// <returns></returns>
    private IEnumerator Flow(GameAction action, Action OnFlowFinished = null)
    {
        // ========== PHASE 1: PRE-ACTION (执行前阶段) ==========
        // 1. 准备：将当前要处理的反应列表指向动作的 PreReactions
        reactions = action.PreReactions;
        // 2. 通知：执行所有全局注册的 Pre 订阅者（监听者）
        PerformSubscribers(action, preSubs);
        // 3. 执行：执行动作自身所有的 PreReactions（这些反应本身也是GameAction）
        yield return PerformReactions();

        // ========== PHASE 2: PERFORM-ACTION (执行阶段) ==========
        // 1. 准备：将反应列表指向动作的 PerformReactions
        reactions = action.PerformReactions;
        // 2. 执行：查找并执行此类型动作注册的核心执行器 (Performer)
        yield return PerformPerformer(action);
        // 3. 执行：执行动作自身所有的 PerformReactions
        yield return PerformReactions();

        // ========== PHASE 3: POST-ACTION (执行后阶段) ==========
        // 1. 准备：将反应列表指向动作的 PostReactions
        reactions = action.PostReactions;
        // 2. 通知：执行所有全局注册的 Post 订阅者（监听者）
        PerformSubscribers(action, postSubs);
        // 3. 执行：执行动作自身所有的 PostReactions
        yield return PerformReactions();

        // 整个动作流程结束，通知调用者（最终会触发Perform方法中的回调，重置IsPerforming）
        OnFlowFinished?.Invoke();
    }


    private IEnumerator PerformPerformer(GameAction action)
    {
        Type type = action.GetType();
        if(performers.ContainsKey(type))
        {
            //<Type, Func<GameAction, IEnumerator>>
            yield return performers[type](action);
        }
    }


    private void PerformSubscribers(GameAction action, Dictionary<Type, List<Action<GameAction>>> subs)
    {
        Type type = action.GetType();
        if(subs.ContainsKey(type))
        {
            // type(GameAction) => List<Action<GameAction>>
            foreach (var sub in subs[type])
            {
                sub(action); //执行委托函数
            }
        }
    }

    private IEnumerator PerformReactions()
    {
        foreach(var reaction in reactions)
        {
            yield return Flow(reaction);
        }
    }


    /// <summary>
    /// 为特定类型的 GameAction 注册或替换一个执行器（Performer）。
    /// 执行器是一个协程函数，定义了如何具体执行该类型的游戏动作。
    /// </summary>
    /// <typeparam name="T">要注册执行器的 GameAction 的具体类型（例如 AttackAction、MoveAction）。</typeparam>
    /// <param name="performer">
    /// 一个协程委托，接受一个类型为 T 的动作参数并返回 IEnumerator。此委托应包含执行该类型动作的所有逻辑（如播放动画、计算效果、等待等）。
    /// </param>
    public static void AttachPerformer<T>(Func<T, IEnumerator> performer) where T : GameAction
    {
        // 获取动作类型的 Type 对象，用作字典的键
        Type type = typeof(T);

        /* 
         * 创建一个包装器委托，将通用的 GameAction 参数转换为具体的类型 T
         * 这是必要的，因为 performers 字典需要统一的 GameAction -> IEnumerator 签名
         * 代码使用了Lambda表达式
         * performer((T)action) 执行委托函数并返回IEnumerator
         */
        IEnumerator wrappedPerformer(GameAction action) => performer((T)action);

        // 检查是否已存在该类型的执行器
        if (performers.ContainsKey(type))
            performers[type] = wrappedPerformer; // 存在则替换（更新）
        else
            performers.Add(type, wrappedPerformer); // 不存在则添加
    }

    public static void DetachPerformer<T>() where T : GameAction
    {
        Type type = typeof(T);
        if(performers.ContainsKey(type))
            performers.Remove(type);
    }


    /// <summary>
    /// 与AttachPerformer类似,向Subs字典中更新GameAction对应的监听者列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="reaction"></param>
    /// <param name="timing"></param>
    public static void SubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Type type = typeof(T);

        //选取对应的反应时机字典
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;

        Action<GameAction> wrapped = (a) => reaction((T)a);
        reactionMap[reaction] = wrapped;

        if (!subs.ContainsKey(type))
            subs[type] = new();

        subs[type].Add(wrapped);
        //void wrappedReaction(GameAction action) => reaction((T)action);

        //if(subs.ContainsKey(type))
        //{
        //    subs[type].Add(wrappedReaction);
        //}
        //else
        //{
        //    subs.Add(type, new());
        //    subs[type].Add(wrappedReaction);
        //}

    }


    public static void UnsubscribeReaction<T>(Action<T> reaction, ReactionTiming timing) where T : GameAction
    {
        Type type = typeof(T);
        
        Dictionary<Type, List<Action<GameAction>>> subs = timing == ReactionTiming.PRE ? preSubs : postSubs;
        
        // wrapped 直接从字典中获取,确保能够真正删除
        if (reactionMap.TryGetValue(reaction, out var wrapped))
        {
            if (subs.ContainsKey(type))
                subs[type].Remove(wrapped);

            reactionMap.Remove(reaction);
        }

        // 原来的代码无法真正删除wrapped
        //if(subs.ContainsKey(type))
        //{
        //    void wrappedReaction(GameAction action) => reaction((T)action);
        //    subs[type].Remove(wrappedReaction);
        //}

    }

}
