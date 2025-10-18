using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUI : MonoBehaviour, IUIMove
{
    //NOTE: 组合由于继承,该UI能够移动
    private UIMoveComponent uiMoveComponent;

    private void Awake()
    {
        uiMoveComponent = GetComponentInChildren<UIMoveComponent>();
        if (uiMoveComponent == null)
            Debug.LogError("未发现UIMoveComponent组件!");
    }

    private void Update()
    {

    }

    /// <summary>
    /// 外部实际如此引用
    /// </summary>
    public void MoveUI()
    {
        uiMoveComponent.SwitchModeMoveUI();
    }
}
