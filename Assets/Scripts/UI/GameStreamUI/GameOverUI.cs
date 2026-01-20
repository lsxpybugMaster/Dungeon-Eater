using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : TextBtnUI
{
    protected override void BindBtnInbtnList(List<Button> btnList)
    {
        BindBtn(0, () => {
            GameManager.Instance?.GlobalClearLogic();
            SceneManager.LoadScene("TitleScene");
        });

        BindBtn(1, () => { Debug.Log("按下了按钮1"); });
    }
}
