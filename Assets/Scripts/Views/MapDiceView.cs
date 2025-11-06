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
//MapDiceView(V) MapDice(M) MapController(IoC)
//控制反转（Inversion of Control, IoC）" 下层模块不依赖上层模块，控制流程交由外部统一管理。"
//IMPORTANT: 既然是View, 那么就不要再额外创建MapData副本,所有数据修改都由Data控制
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

    //NOTE: 基于事件的控制反转IoC
    public event Action<MapDiceView> OnDiceClicked;
    public event Action<MapDiceView> OnDiceMoveFinished;

    //更新骰子在地图上位置
    public void SetIndex(int index) => MapDice.Index = index;


    public void UpdateDiceRollText()
    {
        Debug.Log($"视图层: {MapDice.Point}");
        diceRollText.text = MapDice.Point.ToString();
    }

    private void Update()
    {
        OnMouseHover();   
    }

    //NOTE: 实例化后调用
    public void Init()
    {
        stepMoveSpeed = MapControlSystem.Instance.MapDiceMoveSpeed;
        step = MapControlSystem.Instance.Step;
        UpdateDiceRollText();
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

        //准备动画序列
        Sequence seq = DOTween.Sequence();

        Vector3 tarPos = transform.position;
        int dbg = 1;
        foreach (char ch in directions.ToUpper())
        {
            tarPos += GL.Direct[ch] * step;
            seq.Append(
                transform.DOMove(tarPos, stepMoveSpeed)
                .OnComplete(() => {Debug.Log($"==========={dbg}========="); MapDice.DecreasePoint(1); UpdateDiceRollText(); })
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
