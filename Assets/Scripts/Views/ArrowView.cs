using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowView : MonoBehaviour
{
    [SerializeField] private GameObject arrowHead;

    [SerializeField] private LineRenderer lineRenderer;

    public Vector3 startPosition;

    private void Update()
    {
        // 获取鼠标在当前帧的世界坐标位置，作为箭头的目标终点位置
        Vector3 endPosition = MouseUtil.GetMousePositionInWorldSpace();

        // 使用上一帧的位置,移动更平滑
        // 计算从起始点指向箭头头部的方向向量，并进行归一化处理
        Vector3 direction = -(startPosition - arrowHead.transform.position).normalized;

        // 将终点位置向反方向偏移0.5个单位，避免箭头头部与线条终点重叠
        lineRenderer.SetPosition(1, endPosition - direction * 0.5f);

        // 将箭头头部移动到鼠标所在的世界坐标位置
        arrowHead.transform.position = endPosition;

        // 旋转箭头头部，使其右方向对准计算出的方向向量
        arrowHead.transform.right = direction;
    }

    public void SetupArrow(Vector3 startPosition)
    {
        this.startPosition = startPosition;
        //修改箭头线的两个端点位置
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, MouseUtil.GetMousePositionInWorldSpace());
    }
}
