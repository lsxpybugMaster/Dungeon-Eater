using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 控制战斗信息显示框的UI信息
/// </summary>
public class BattleInfoUI : Singleton<BattleInfoUI>
{
    [SerializeField] private TMP_Text info_TMP;
    [SerializeField] private ScrollRect scrollRect; //文本窗口部分

    private void AddLine(string line)
    {
        info_TMP.text += line + "\n";
        ScrollToBottom();
    }

    // 滑动到底部的方法
    private void ScrollToBottom()
    {
        if (scrollRect != null)
        {
            // 使用协程确保在文本渲染完成后滚动
            StartCoroutine(ScrollToBottomCoroutine());
        }
    }

    private IEnumerator ScrollToBottomCoroutine()
    {
        // 等待一帧，确保文本已经更新并完成布局计算
        yield return new WaitForEndOfFrame();

        // 设置垂直位置为0（底部）
        scrollRect.verticalNormalizedPosition = 0f;

        // 强制Canvas更新，确保滚动立即生效
        Canvas.ForceUpdateCanvases();
    }


    /// <summary>
    /// 数值掷骰
    /// </summary>
    public void AddThrowResult(int ans, string originCommand, string addtionInfo = "")
    {
        AddLine($"{addtionInfo} Throws: <b><color=black>{ans} ({originCommand})</color></b>");
    }


    /// <summary>
    /// 检定掷骰
    /// </summary>
    public void AddSuccessResult(int ans, int dc, string originCommand, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=green>Success! {ans}({originCommand}) > {dc}</color>");       
    }

    public void AddSuccessResult(int ans, int add, int sub, int dc, string originCommand, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=green>Success! {ans}({originCommand}) + {add} - {sub} = {ans + add - sub} > {dc} </color>");
    }

    public void AddGiantSuccessResult(int ans, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=orange>Gaint Success! ({ans}) </color>");
    }


    public void AddGiantFailedResult(int ans, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=red>Gaint Failed! {ans} </color>");
    }


    public void AddFailedResult(int ans, int dc, string originCommand, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=red>Failed! {ans}({originCommand}) < {dc} </color>");
    }

    public void AddFailedResult(int ans, int add, int sub ,int dc, string originCommand, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=red>Failed! {ans}({originCommand}) + {add} - {sub} = {ans + add - sub} < {dc} </color>");
    }



    public void AddFixedResult(int ans, CombatantView caster)
    {
        AddLine($"[{caster.gameObject.name}] <color=blue>Deal {ans} damage</color>");
    }
}
