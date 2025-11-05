using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 地图上的骰子,主要控制玩家的地图探索
/// 
/// </summary>
//NOTE: MapDiceView(V) MapDice(M) MapController(IoC)
//DISCUSS: 点击骰子后触发的功能还是上报给上层计算好
//STEP: 控制反转（Inversion of Control, IoC）" 下层模块不依赖上层模块，控制流程交由外部统一管理。"
public class MapDiceView : MonoBehaviour
{
    [SerializeField] private float hover_and_rotate_speed = 60f;
    [SerializeField] private TMP_Text diceRollText;

    private bool isMoveHovering = false;
    //BUG: 不能在这里直接初始化
    private float stepMoveSpeed;
    private float step;

    //IoC, 因此数据MapDice需要完全暴露给上层
    public MapDice MapDice { get; set; }
    public void SetIndex(int index) => MapDice.Index = index;

    //NOTE: 基于事件的控制反转IoC
    public event Action<MapDiceView> OnDiceClicked;
    public event Action<MapDiceView> OnDiceMoveFinished;

    //如果为0则清除字符串
    private void SetDiceRollText(int x) => diceRollText.text = (x == 0) ? "" : x.ToString();
  

    private void Update()
    {
        OnMouseHover();   
    }

    private void OnEnable()
    {
        stepMoveSpeed = MapControlSystem.Instance.MapDiceMoveSpeed;
        step = MapControlSystem.Instance.Step;
    }

    private void OnMouseHover()
    {
        if (isMoveHovering)
        {
            transform.Rotate(0, 0, hover_and_rotate_speed * Time.deltaTime);
        }
    }

    private void OnMouseDown()
    {
        //STEP: 通过IoC将逻辑上传,由上面的系统管理逻辑
        OnDiceClicked.Invoke(this);
    }

    /// <summary>
    /// 上层模块传入字符串控制移动逻辑
    /// </summary>
    /// <param name="directions">目前是U,D,L,R</param>
    public void MoveToTarget(string directions)
    {
        int diceRoll = directions.Length;
        SetDiceRollText(diceRoll);
        //准备动画序列
        Sequence seq = DOTween.Sequence();

        Vector3 tarPos = transform.position;
        foreach (char ch in directions.ToUpper())
        {
            tarPos += GL.Direct[ch] * step;
            seq.Append(
                transform.DOMove(tarPos, stepMoveSpeed)
                .OnComplete(() => SetDiceRollText(--diceRoll))
                /* 后续补充方向
                .SetEase(Ease.InOutQuad)
                .OnComplete(() => OnStepReached?.Invoke(nextPos))
                */
            );
        }

        //NOTE: 及时通知上层模块已经到达位置
        seq.OnComplete(() => OnDiceMoveFinished?.Invoke(this));
        seq.Play();
    }

    //注意要求有碰撞体才能运行
    private void OnMouseEnter()
    {
        isMoveHovering = true;
    }

    private void OnMouseExit() 
    { 
        isMoveHovering = false;
    }
}
