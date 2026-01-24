using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : MonoBehaviour
{
    private TMP_Text turnTMP;

    //IMPORTANT 通过协程保证TurnSystem先注册
    private void OnEnable()
    {
        turnTMP = GetComponent<TMP_Text>();
    }

    private void OnDestroy()
    {
        if (TurnSystem.Instance != null)
            TurnSystem.Instance.OnTurnChanged -= UpdateTurnTMP;
    }

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += UpdateTurnTMP;
        UpdateTurnTMP(TurnSystem.Instance.Turn);
    }

    private void UpdateTurnTMP(int turn)
    {
        turnTMP.text = $"Turn : {turn}"; 
    }

}
