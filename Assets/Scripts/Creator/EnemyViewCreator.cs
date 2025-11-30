using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//专门用来在游戏界面中生成敌人
public class EnemyViewCreator : Singleton<EnemyViewCreator>
{
    [SerializeField] private EnemyView enemyViewPrefab;

    public EnemyView CreateEnemyView(EnemyData enemyData, Vector3 position, Quaternion rotation)
    {
        //现在需要同时创建 M, V
        EnemyView enemyView = Instantiate(enemyViewPrefab, position, rotation);
        EnemyCombatant enemyCombatant = new(enemyData);
        enemyView.Setup(enemyData, enemyCombatant);
        return enemyView;
    }

}
