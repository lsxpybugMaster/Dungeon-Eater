using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// 理解为Entity脚本,控制所有游戏角色的逻辑(如生命)
/// </summary>
public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text profText;
    [SerializeField] private TMP_Text flexText;

    //对象图片
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private StatusEffectsUI statusEffectsUI;

    public Combatant Combatant { get; set; }

    //FIXME: 实际使用时需要强制转换,不太方便,不使用则需要每个子类声明一次,也不方便
    public Combatant M => Combatant;

    /*
        View初始化逻辑
     */
    protected virtual void Setup(Sprite image, Combatant combatant)
    {
        Combatant = combatant;
        
        //TODO: 临时绑定,目前已经破坏了MVC!! 
        Combatant.__view__ = this;

        BindEvents();
        
        spriteRenderer.sprite = image;

        UpdateHealthText(M.CurrentHealth, M.MaxHealth);
        UpdateOtherText();
    }

    protected virtual CombatantView ReturnThis()
    {
        return this;
    }

    protected virtual void BindEvents()
    { 
        Combatant.OnHealthChanged += UpdateHealthText;
        Combatant.OnEffectChanged += UpdateEffect;
        Combatant.OnDamaged += Shake;
    }

    protected virtual void UnBindEvents()
    {

        Combatant.OnHealthChanged -= UpdateHealthText;
        Combatant.OnEffectChanged -= UpdateEffect;
        Combatant.OnDamaged -= Shake;
    }


    private void OnDestroy()
    {
        UnBindEvents();
    }


    //protected virtual void UpdateHealthText()
    //{
    //    healthText.text = $"HP: {CurrentHealth}";
    //}

    public virtual void UpdateHealthText(int CurrentHealth, int MaxHealth)
    {
        healthText.text = $"{CurrentHealth}";
    }

    public virtual void UpdateOtherText()
    {
        int prof = Mathf.Max(M.Proficiency + M.ProficiencyBuff, 0);
        int flex = Mathf.Max(M.Flexbility + M.FlexbilityBuff, 0);

        profText.text = $"{prof}";

        flexText.text = $"{flex}";
    }


    public void UpdateEffect(StatusEffectType type, int stacks)
    {
        statusEffectsUI.UpdateStatusEffectUI(type, stacks);

        //生命之外的其他属性是通过状态来修改的,所以改变状态时跟着刷新一次即可
        UpdateOtherText();
    }

    public void Shake()
    {
        transform.DOShakePosition(Config.Instance.effectTime, 0.5f);
    }
}
