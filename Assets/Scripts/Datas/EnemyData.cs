using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//参考CardData的设计
[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : CombatantData, IHaveKey<string>
{
    // 与生成有关的配置数据,动态数据会分析这些信息,决定是否包含这些内容。
    [Header("生成配置")]
    public string ID;
    [field: SerializeField] public int Diffculty { get; private set; }
    [field: SerializeField] public List<int> AppearLevels { get; private set; }

    public string GetKey() => ID;

    // 基本数据 => 已存储至 CombatantData

    //更细粒度的攻击数值
    [field: SerializeField] public int FixedAttack { get; private set; }
    [field: SerializeField] public string LightAttackStr { get; private set; }
    [field: SerializeField] public string HeavyAttackStr { get; private set; }

    // AI逻辑数据
    [field: SerializeReference, SR]
    public List<EnemyIntendNode> ConditionedIntendTable { get; private set; }


    [field: SerializeReference, SR]
    public List<EnemyIntendNode> RandomIntendTable { get; private set; }
    /*
        简单行为树逻辑:
        条件行为: 按顺序判断，依次执行直到第一个事件执行成功
        随机行为: 如果条件行为没有选出事件,则在随机行为中按概率抽取行为(概率和为1)
     */
#if UNITY_EDITOR
    private void OnValidate()
    {
        // 获取 asset 路径
        string path = AssetDatabase.GetAssetPath(this);
        if (string.IsNullOrEmpty(path)) return;

        // 文件名（不含扩展名）
        string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

        ID = fileName;

        // 告诉数据已经修改,写回磁盘
        EditorUtility.SetDirty(this);
    }
#endif
}
