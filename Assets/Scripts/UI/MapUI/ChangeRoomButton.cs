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

        //BUG: 非常重要,保证不会重复注册事件!!
        Btn.onClick.RemoveAllListeners();
     
        Btn.onClick.AddListener(() =>
        {
            ChangeRoomSystem.Instance.EnterRoom(mapGrid);

            //禁用按钮
            MapInteractions.OnButtonDisabled();
        });
    }

    //BUG: 注意一旦房间结束功能,需要禁用按钮!!
    //IMPORTANT: 不要让经常disabled的UI对象监听事件,因为其disable时监听不到!
    //public void DisableButton()
    //{
    //    Debug.Log("DISABLE");
    //    Btn.onClick.RemoveAllListeners();
    //}

}
