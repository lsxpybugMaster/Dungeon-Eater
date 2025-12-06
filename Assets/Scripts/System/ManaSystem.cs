using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaSystem : Singleton<ManaSystem>
{
    [SerializeField] private ManaUI manaUI;

    private int maxMana = 3;

    private int currentMana;

    /// <summary>
    /// 初始化能量
    /// </summary>
    public void Setup(int maxMana)
    {
        this.maxMana = maxMana;
        currentMana = maxMana;
        manaUI.UpdateManaText(currentMana);
    }

    void OnEnable()
    {
        ActionSystem.AttachPerformer<SpendManaGA>(SpendManaPerformer);
        ActionSystem.AttachPerformer<RefillManaGA>(RefillManaPerformer);
        ActionSystem.AttachPerformer<AddManaGA>(AddManaPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<SpendManaGA>();  
        ActionSystem.DetachPerformer<RefillManaGA>();
        ActionSystem.DetachPerformer<AddManaGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }



    public bool HasEnoughMana(int mana)
    {
        return currentMana >= mana;
    }


    private IEnumerator SpendManaPerformer(SpendManaGA spendManaGA)
    {
        currentMana -= spendManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }    


    private IEnumerator RefillManaPerformer(RefillManaGA refillManaGA)
    {
        currentMana = maxMana;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }

    private IEnumerator AddManaPerformer(AddManaGA addManaGA)
    {
        if (addManaGA.Refill)
            currentMana = maxMana;
        else
            currentMana += addManaGA.Amount;
        manaUI.UpdateManaText(currentMana);
        yield return null;
    }


    //注意这是个反应而非Performer!!!
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        RefillManaGA refillManaGA = new();
        ActionSystem.Instance.AddReaction(refillManaGA);
    }
}
 