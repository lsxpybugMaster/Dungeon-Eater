using DG.Tweening;                 // 引入 DOTween 动画库
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;          // 引入 Unity 的 Spline 系统

public class HandView : MonoBehaviour
{
    //手牌之间的展示间隔
    [SerializeField][Range(0,0.2f)] private float cardInterval = 0.1f;

    [SerializeField] private float updateCardPostionTime = 0.15f;

    [SerializeField] private SplineContainer splineContainer;
    // Inspector 面板里指定的样条容器 (SplineContainer)，用来决定卡牌排列路径

    private readonly List<CardView> cards = new();
    // 存储所有手牌 (CardView)，只读引用，内部可添加


    /// <summary>
    /// 向手牌中添加一张卡，并更新所有卡的位置与朝向
    /// </summary>
    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);  // 把卡加入手牌
        yield return UpdateCardPositions(updateCardPostionTime); // 用 0.15 秒的动画重新排列
    }



    /// <summary>
    /// 移除手牌中card对应的CardView并返回该CardView
    /// </summary>
    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null) return null;

        cards.Remove(cardView);
        //cards列表元素变化后,执行协程重新计算各卡牌位置
        StartCoroutine(UpdateCardPositions(updateCardPostionTime));
        return cardView;
    }



    /// <summary>
    /// 辅助函数,从Card(Model)中找到其对应的CardView(View)
    /// </summary>
    public CardView GetCardView(Card card)
    {
        //Where中Lambda表达式用于寻找满足条件的第一个
        return cards.Where(cardView => cardView.Card == card).FirstOrDefault();
    }



    /// <summary>
    /// 根据当前卡牌数量，重新计算并更新它们在样条曲线上的位置和朝向
    /// </summary>
    private IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;  // 没有卡牌时直接退出

        // 卡牌之间在曲线参数上的间隔（这里固定为 0.1）
        float cardSpacing = cardInterval;

        // 第一张卡的起始位置（以 0.5 为中心，左右对称分布）
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;

        // 获取样条对象
        Spline spline = splineContainer.Spline;

        // 遍历所有卡，依次计算它们的目标位置与旋转
        for (int i = 0; i < cards.Count; i++)
        {
            // 在样条上的参数 (0~1)，决定卡牌位置
            float p = firstCardPosition + i * cardSpacing;

            // 计算样条上的位置、切线方向、法向量
            Vector3 splinePosition = spline.EvaluatePosition(p);   // 曲线上的位置
            Vector3 forward = spline.EvaluateTangent(p);           // 曲线方向（切线）
            Vector3 up = spline.EvaluateUpVector(p);               // 曲线上方向（法线）

            // 计算卡牌旋转：让卡牌面朝玩家
            Quaternion rotation = Quaternion.LookRotation(
                -up,                                              // 前方向（卡牌面朝上方的反方向）
                Vector3.Cross(-up, forward).normalized            // 上方向 = (-up × forward)
            );

            // 使用 DOTween 将卡牌移动到曲线位置
            cards[i].transform.DOMove(
                splinePosition + transform.position + 0.01f * i * Vector3.back, // 避免 Z-fighting，逐张往后挪一点
                duration                                                        // 动画时长
            );

            // 使用 DOTween 将卡牌旋转到目标角度
            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        // 等待动画完成（保证协程流程同步）
        yield return new WaitForSeconds(duration);
    }
}
