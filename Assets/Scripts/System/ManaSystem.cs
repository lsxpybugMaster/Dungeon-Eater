using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private ManaUI manaUI;

    // 最基本的行动点
    private int maxMana = 3;
    private int currentMana;

    // 特殊行动点: 战斗大师点数: 只会获取无法补充
    // private int maxCombatMasterPoint = 5; 
    private int currentCombatMasterPoint = 0;

    // 很后期的代码 : 法师的法术位 (可能无法实现)
    // private int maxMagic = 4;
    // private int curMagic;

    /// <summary>
    /// 初始化能量
    /// </summary>
    public void Setup(int maxMana)
    {
        this.maxMana = maxMana;
        currentMana = maxMana;
        currentCombatMasterPoint = 0;
        manaUI.UpdateManaText(currentMana);
        manaUI.UpdateOtherManaText(currentCombatMasterPoint, ManaID.CombatMaster);
    }


    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<SpendOtherManaGA>(SpendOtherManaPerformer);

        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);

        ActionSystem.AttachPerformer<AddManaGA>(AddManaPerformer);
        ActionSystem.AttachPerformer<AddOtherManaGA>(AddOtherManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }


    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();  
        ActionSystem.DetachPerformer<SpendOtherManaGA>();

        ActionSystem.DetachPerformer<RefillManaGA>();

        ActionSystem.DetachPerformer<AddManaGA>();
        ActionSystem.DetachPerformer<AddOtherManaGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }


    public bool HasEnoughMana(int mana, ManaID manaID)
    {
        int theMana = manaID switch
        {
            ManaID.BasicMana => currentMana,
            ManaID.CombatMaster => currentCombatMasterPoint,
            _ => -1,
        };

        return theMana >= mana;
    }


    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }


    // 目前一段时间内不会有新的属性, 所以先if-else放着,以后改为字典
    private IEnumerator SpendOtherManaPerformer(SpendOtherManaGA spendOtherManaGA)
    {
        ManaID manaID = spendOtherManaGA.ManaType;
        if (manaID == ManaID.CombatMaster)
        {
            currentCombatMasterPoint -= spendOtherManaGA.Amount;
            manaUI.UpdateOtherManaText(currentCombatMasterPoint, manaID);
            yield return null;
        }
    }


    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana = maxMana;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }


    private IEnumerator AddManaPerformer(AddManaGA addManaGA)
    {
        yield return AnimStatic.JumpAnim();

        if (addManaGA.Refill)
            currentMana = maxMana;
        else
            currentMana += addManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }


    // 目前一段时间内不会有新的属性, 所以先if-else放着,以后改为字典
    private IEnumerator AddOtherManaPerformer(AddOtherManaGA ga)
    {
        yield return AnimStatic.JumpAnim();

        ManaID manaId = ga.ManaID;
        if (manaId == ManaID.CombatMaster)
        {
            currentCombatMasterPoint += ga.Amount;
        }
        manaUI.UpdateOtherManaText(currentCombatMasterPoint, manaId);
        yield return null;
    }


    //注意这是个反应而非Performer!!!
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.AddReaction(refillManaGA);
    }
}
 