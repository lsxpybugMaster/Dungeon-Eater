using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeRoomButton : MonoBehaviour
{
    private TextMeshProUGUI textInfoTMP;
    private Button btn;

    private void Awake()
    {
        textInfoTMP = GetComponentInChildren<TextMeshProUGUI>();
        btn = GetComponent<Button>();
    }

    //在玩家棋子落定后,决定按钮的流向
    public void Setup(string text, GridType gridType)
    {
        textInfoTMP.text = text;
        btn.onClick.AddListener(() =>
        {
            ChangeRoomSystem.Instance.EnterRoom(gridType);
        });
    }

}
