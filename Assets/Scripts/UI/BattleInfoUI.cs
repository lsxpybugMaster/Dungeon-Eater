using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 控制战斗信息显示框的UI信息
/// </summary>
public class BattleInfoUI : Singleton<BattleInfoUI>
{
    [SerializeField] private TMP_Text info_TMP;

    private void AddLine(string line)
    {
        info_TMP.text += line + "\n";
    }

    public void AddThrowResult(int ans, string originCommand)
    {
        AddLine($"Throws: {ans} ({originCommand})");
    }

    public void AddFailedResult(int ans, int dc, string originCommand)
    {
        AddLine($"<color=red>Faid: {ans}({originCommand}) < {dc} </color>");
    }
}
