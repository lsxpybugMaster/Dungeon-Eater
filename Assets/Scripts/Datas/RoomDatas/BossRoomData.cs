using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/BossRoomData")]
public class BossRoomData : EnemyRoomData
{
    protected override List<EnemyData> GetEnemies(MapGrid grid)
    {
        return enemyGroupGenerator.GeneraterBoss(grid);
    }
}
