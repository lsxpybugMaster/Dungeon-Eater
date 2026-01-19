using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameOverUI : TextBtnUI
{
    protected override void BindBtnInbtnList(List<Button> btnList)
    {
        BindBtn(0, () => { Debug.Log("按下了按钮0"); });

        BindBtn(1, () => { Debug.Log("按下了按钮1"); });
    }
}
