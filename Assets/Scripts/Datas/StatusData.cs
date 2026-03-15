using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 之前的StatusEffect没有做成data,
/// 现在需要改进成StatusData, 以支持外部数据配置
/// </summary>
[CreateAssetMenu(menuName = "Data/StatusData")]
public class StatusData : ScriptableObject, IHaveKey<StatusEffectType>
    ,IHaveTooltip
{

    public StatusEffectType Type;
    public StatusEffectType GetKey() => Type;

    public Sprite sprite;

    [field: SerializeField] public string NameKey { get; private set; }
    [field: SerializeField] public string DescriptionKey { get; private set; }

    //支持外部数据配置
    [field: SerializeReference, SR] public Effect effectOnTurnEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 该状态在回合开始时造成的影响(燃烧,眩晕)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnStart(Combatant c)
    {
    }


    /// <summary>
    /// 状态触发时造成的影响(如额外伤害)
    /// </summary>
    public virtual void OnTriggerd(Combatant c)
    {

    }

    /// <summary>
    /// 该状态在回合结束时造成的影响(回复生命,结算状态)
    /// </summary>
    /// <param name="c"></param>
    public virtual void OnTurnEnd(Combatant c)
    {
        if (effectOnTurnEnd != null)
        {
            GameAction ga = effectOnTurnEnd.GetGameAction(new() { c.__view__ }, c.__view__, null);
            // 该函数会在其他Performer中执行,所以需要加Reaction而非Performer
            ActionSystem.Instance.AddReaction(ga);
        }
    }

    public TooltipData GetTooltipData(int statusStack)
    {
        string name = LocalizationManager.Instance.Get(NameKey);
        string desc = LocalizationManager.Instance.Get(DescriptionKey, statusStack);
        return new TooltipData(name, desc);
    }

    public TooltipData GetTooltipData(params object[] args)
    {
        return GetTooltipData((int)args[0]);
    }
}
