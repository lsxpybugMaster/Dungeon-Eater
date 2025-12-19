using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//包装一层,因为Unity不支持二维数组序列化
[Serializable]
public class EnemyGroup
{
    [field: SerializeField]
    public List<EnemyData> Enemies { get; set; }
}

[CreateAssetMenu(menuName = "Data/EnemyGroupData")]
//存储一些搭配好的固定敌人
public class EnemyGroupsData : ScriptableObject, IHaveKey<int>
{
    //属于哪一层关卡
    [SerializeField] int level;
    //代表其中搭配好的固定敌人(二维列表,为了便于配置提出了一层作为EnemyGroup)
    [field: SerializeField] public List<EnemyGroup> Groups { get; set; }

    public int GetKey() => level;


#if UNITY_EDITOR
    //快速配置id
    //private void OnValidate()
    //{
    //    // 获取 asset 路径
    //    string path = AssetDatabase.GetAssetPath(this);
    //    if (string.IsNullOrEmpty(path)) return;

    //    // 文件名（不含扩展名）
    //    string fileName = System.IO.Path.GetFileNameWithoutExtension(path);

    //    // 方案 A：文件名本身就是数字
    //    if (int.TryParse(fileName, out int parsed))
    //    {
    //        id = parsed;
    //    }
    //    else
    //    {
    //        // 方案 B：对文件名做 Hash，稳定且不冲突概率极低
    //        id = fileName.GetHashCode();
    //    }

    //    EditorUtility.SetDirty(this);
    //}
#endif
}


