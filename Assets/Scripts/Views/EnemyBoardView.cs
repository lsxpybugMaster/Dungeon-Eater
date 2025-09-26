using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理游戏中的所有敌人
/// </summary>
public class EnemyBoardView : MonoBehaviour
{
    //存储所有敌人位置对象
    [SerializeField] private List<Transform> slots;

    /// <summary>
    /// 存储所有敌人对象
    /// </summary>
    public List<EnemyView> EnemyViews { get; private set; } = new();

    public void AddEnemy(EnemyData enemyData)
    {
        Transform slot = slots[EnemyViews.Count];
        //生成对象实例 + 挂载在父对象上 
        EnemyView enemyView = EnemyViewCreator.Instance.CreateEnemyView(enemyData, slot.position, slot.rotation);
        enemyView.transform.parent = slot;
        EnemyViews.Add(enemyView);
    }

    //敌人死亡时从列表删除
    public IEnumerator RemoveEnemy(EnemyView enemyView)
    {
        EnemyViews.Remove(enemyView);
        Tween tween = enemyView.transform.DOScale(Vector3.zero, 0.25f);
        yield return tween.WaitForCompletion();
        Destroy(enemyView.gameObject);
    }
}
