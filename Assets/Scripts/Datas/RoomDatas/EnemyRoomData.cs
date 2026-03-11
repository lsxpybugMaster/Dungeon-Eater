using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/RoomData/EnemyRoomData")]
public class EnemyRoomData : RoomData
{
    [field: SerializeField] protected BattleType battleType;

    protected EnemyGroupGenerator enemyGroupGenerator;
    protected int currentDifficulty;

    //在SO文件中配置函数
    public override void Enter(MapGrid grid)
    {
        enemyGroupGenerator = new EnemyGroupGenerator();
        currentDifficulty = GameManager.Instance.LevelProgress.Difficulty;

        Debug.Log($"DIFF {currentDifficulty}");

        grid.roomEnemies = GetEnemies(grid);

        GameManager.Instance.EnemyPool.SetEnemiesBuffer(grid.roomEnemies);
        GameManager.Instance.SceneModeManager.ToBattleMode(battleType);
    }

    /// <summary>
    /// 依据子类房间类型不同, 生成不同的敌人组合
    /// </summary>
    protected virtual List<EnemyData> GetEnemies(MapGrid grid)
    {
        return enemyGroupGenerator.GenerateEnemies(grid, currentDifficulty);
    }
}
