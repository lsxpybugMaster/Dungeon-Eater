using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    
    [SerializeField] private TMP_Text description;
    
    [SerializeField] private TMP_Text mana;
    
    [SerializeField] private SpriteRenderer imageSR;

    //卡牌作用区碰撞体的LayerMask
    [SerializeField] private LayerMask dropLayer;
    
    /// <summary>
    /// 卡牌游戏对象
    /// </summary>
    [SerializeField] private GameObject wrapper;

    public Card Card { get; private set; }

    private Vector3 dragStartPosition;

    private Quaternion dragStartRotation;


    /// <summary>
    /// 根据卡牌数据更新信息
    /// </summary>
    /// <param name="card">卡牌数据</param>
    public void Setup(Card card)
    {
        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        imageSR.sprite = card.Image;
    }

    //将交互判断鉴权提出
    bool CanHover()
    {
        return Interactions.Instance.PlayerCanHover() 
            && GameManager.Instance.GameState != GameState.BattleVictory;
    }

    bool CanInterAct()
    {
        return Interactions.Instance.PlayerCanInteract()
            && GameManager.Instance.GameState != GameState.BattleVictory;
    }

    #region 鼠标悬浮系统
    //注意要求有碰撞体才能运行
    void OnMouseEnter()
    {
        //玩家在拖动卡牌时,不执行后续逻辑
        //胜利结算时不执行后续逻辑
        if (!CanHover()) return;

        wrapper.SetActive(false);
        Vector3 pos = new(transform.position.x, -2, 0);
        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    void OnMouseExit()
    {
        //玩家在拖动卡牌时,不执行后续逻辑
        if (!CanHover()) return;
        CardViewHoverSystem.Instance.Hide();

        wrapper.SetActive(true);
    }
    #endregion


    #region 拖拽系统
    void OnMouseDown()
    {
        //有动作在执行时,禁止玩家交互卡牌
        if (!CanInterAct()) return;
        
        if (Card.ManualTargetEffect != null)
        {
            //如果是需要手动控制的，需要显示箭头
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            Interactions.Instance.PlayerIsDragging = true;

            //显示手牌
            wrapper.SetActive(true);
            //隐藏卡牌展示UI
            CardViewHoverSystem.Instance.Hide();

            dragStartPosition = transform.position;
            dragStartRotation = transform.rotation;

            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }
        
    }


    void OnMouseDrag()
    {
        //有动作在执行时,禁止玩家交互卡牌
        if (!CanInterAct()) return;
        
        //如果显示了箭头,我们不需要再拖动卡牌了
        if (Card.ManualTargetEffect != null) return;

        //使卡牌位置时刻跟随鼠标位置,实现拖拽功能
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }


    void OnMouseUp()
    {
        //有动作在执行时,禁止玩家交互卡牌
        if (!CanInterAct()) return;
        
        if (Card.ManualTargetEffect != null)
        {
            //如果是通过箭头进行卡牌功能触发,则不再使用射线检测而直接从箭头脚本返回目标
            EnemyView target = ManualTargetSystem.Instance.EndTargeting(MouseUtil.GetMousePositionInWorldSpace(-1));

            if(target != null && ManaSystem.Instance.HasEnoughMana(Card.Mana))
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
        }
        else
        {
            //注意必须卡牌在对应作用区使用才会真正作用
            //要使用3D碰撞体!!!!!!
            //同时注意检查资源是否够
            if (ManaSystem.Instance.HasEnoughMana(Card.Mana)
            && Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, 10f, dropLayer))
            {
                //执行卡牌功能,该GA初始化时需额外传入参数,为所要删除的卡牌
                PlayCardGA playCardGA = new(Card);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                //玩家松手时,卡牌并未落在其能实现功能的位置,因此需要回到手牌中显示
                transform.position = dragStartPosition;
                transform.rotation = dragStartRotation;
            }
            Interactions.Instance.PlayerIsDragging = false;
        }
    }

    #endregion
}
