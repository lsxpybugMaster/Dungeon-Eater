using TMPro;
using UnityEngine;
using UIUtils;
using System.Collections.Generic;

//注意只保留额外的 ID
public enum ManaID
{
    BasicMana = 999, //这个只作为基本的默认
    CombatMaster = 0,
    Magic = 0,
}

//TODO: 这块写的适配度较低,但是目前先以完成为主
public class ManaUI : MonoBehaviour
{
    // 主要资源界面
    [SerializeField] private TMP_Text mana;

    // 次要资源界面 : 使用了简单的自定义 UWithText UI类
    [SerializeField] private List<UWithText> OtherManaUIs;
    
    public void UpdateManaText(int currentMana)
    {
        mana.text = currentMana.ToString();
    }

    public void UpdateOtherManaText(int currentNum, ManaID idType)
    {
        OtherManaUIs[(int)idType].T = currentNum.ToString();
    }
}
