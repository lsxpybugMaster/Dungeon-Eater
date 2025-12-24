using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/EnemyRoomData")]
public class EnemyRoomData : RoomData
{
    //在SO文件中配置函数
    public override void Enter(MapGrid grid)
    {
        GameManager.Instance.EnemyPool.SetEnemiesBuffer(grid.roomEnemies);

        GameManager.Instance.ToBattleMode();
    }
}
