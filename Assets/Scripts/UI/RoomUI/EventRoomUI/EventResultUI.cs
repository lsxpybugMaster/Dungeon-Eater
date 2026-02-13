using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventResultUI : MonoBehaviour
{
    //多长时间改变一次数字变化的速度
    public float showRandomNumberTime = 1f;
    public float numberChangeInterval = 0.1f;

    [SerializeField] private TMP_Text dicePointTMP;
    [SerializeField] private TMP_Text needPointTMP;
    [SerializeField] private TMP_Text eventResultTMP;
   
    /// <summary>
    /// 展示事件判定结果
    /// </summary>
    /// <param name="t">展示骰子数值变化的时间</param>
    public IEnumerator ShowEventResult(int dicePt, int needPt, string eventResult)
    {
        //模拟数字的随机变化
        needPointTMP.text = needPt.ToString();
        int times =(int)(showRandomNumberTime / numberChangeInterval);
        for (int i = 0; i < times; i++)
        {
            dicePointTMP.text = Random.Range(1, 21).ToString();
            yield return new WaitForSeconds(numberChangeInterval);
        }
        dicePointTMP.text = dicePt.ToString();
        eventResultTMP.text = eventResult;
    }


    /// <summary>
    /// 立即展示事件判定结果
    /// </summary>
    public IEnumerator ShowEventResult(string eventResult)
    {
        dicePointTMP.text = "20";
        needPointTMP.text = "0";
        eventResultTMP.text = eventResult;
        yield return null;
    }
}
