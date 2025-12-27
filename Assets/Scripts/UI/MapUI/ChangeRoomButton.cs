using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChangeRoomButton : MonoBehaviour
{
    private TextMeshProUGUI textInfoTMP;
    public Button Btn { get; set; }

    private void OnEnable()
    {
      
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        textInfoTMP = GetComponentInChildren<TextMeshProUGUI>();
        Btn = GetComponent<Button>();
    }

    //在玩家棋子落定后,决定按钮的流向
    public void Setup(string text, MapGrid mapGrid)
    {
        textInfoTMP.text = text;
        Btn.onClick.AddListener(() =>
        {
            ChangeRoomSystem.Instance.EnterRoom(mapGrid);

            MapInteractions.OnMapUIDisabled();
        });
    }

}
