using UnityEngine;

public static class MouseUtil
{
    // 【Attention】 static 类不会随着场景销毁而销毁, 然而引用的 Camera.main 被销毁
    // private static Camera camera = Camera.main;

    /// <summary>
    /// 将屏幕鼠标位置转换为世界空间中的3D坐标
    /// </summary>
    /// <param name="zValue">目标平面的Z轴深度值，默认为0</param>
    /// <returns>鼠标在世界空间中的3D坐标，如果射线与平面无交点则返回Vector3.zero</returns>
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        // 创建与摄像机前方向对齐的平面，位于指定Z深度
        Plane dragPlane = new(Camera.main.transform.forward, new Vector3(0, 0, zValue));

        // 从摄像机发射通过鼠标屏幕位置的射线
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 计算射线与平面的交点
        if (dragPlane.Raycast(ray, out float distance))
        {
            // 返回交点在世界空间中的坐标
            return ray.GetPoint(distance);
        }

        // 如果射线与平面无交点（理论上很少发生），返回零向量
        return Vector3.zero;
    }
}