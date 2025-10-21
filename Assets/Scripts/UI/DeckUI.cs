using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//IDEA: 将其也作为跨场景UI(因为调用其show函数的按钮在GlobalUI上)
public class DeckUI : MonoBehaviour, IUIMove
{
    //NOTE: 组合优于继承,该UI能够移动
    private UIMoveComponent uiMoveComponent;

    //IMPORTANT: 预制体一定要先Instantiate!!
    [SerializeField] private GameObject cardUIPrefab;
    [SerializeField] private GameObject cardUIRoot; //UI放置位置

    private void Awake()
    {
        uiMoveComponent = GetComponentInChildren<UIMoveComponent>();
        if (uiMoveComponent == null)
            Debug.LogError("未发现UIMoveComponent组件!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            MoveUI();
        }
        if (Input.GetKeyDown(KeyCode.L)){
            Show(GameManager.Instance.HeroState.Deck);
        }
    }

    /// <summary>
    /// 外部实际如此引用
    /// </summary>
    public void MoveUI()
    {
        uiMoveComponent.SwitchModeMoveUI();
    }

    /// <summary>
    /// 根据外部传入的卡牌数据展示对应卡牌
    /// </summary>
    private void InstantiateCardUIPrefab(Card card)
    {
        GameObject mygo = Instantiate(cardUIPrefab);
        //挂载到指定父节点下管理
        mygo.transform.SetParent(cardUIRoot.transform);
        mygo.GetComponent<CardUI>().Setup(card);
    }

    public void Show(List<Card> cards)
    {
        foreach (var card in cards)
        {
            InstantiateCardUIPrefab(card);
        }
    }
}
