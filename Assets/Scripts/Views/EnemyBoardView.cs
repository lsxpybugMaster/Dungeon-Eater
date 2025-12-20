using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理游戏中的所有敌人
/// 敌人全部死亡后, 判断胜利
/// </summary>
public class EnemyBoardView : MonoBehaviour
{
    //存储所有敌人位置对象
    [SerializeField] private List<Transform> slots;

    //使用Action-Reaction机制,无需引用
    //[SerializeField] private MatchSetupSystem matchSetupSystem;

    /// <summary>
    /// 存储所有敌人对象
    /// </summary>
    public List<EnemyView> EnemyViews { get; private set; } = new();

    public void AddEnemy(EnemyData enemyData)
    {
        Debug.Log($"COUNT: {EnemyViews.Count}");

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

        // 删除了最后一个敌人, 判断胜利
        if (EnemyViews.Count == 0)
        {
            KillAllEnemyGA killAllEnemyGA = new KillAllEnemyGA();
            //仍旧需要使用反应  因为代码在KillEnemyPerformer中
            ActionSystem.Instance.AddReaction(killAllEnemyGA);
        }
    }
}
