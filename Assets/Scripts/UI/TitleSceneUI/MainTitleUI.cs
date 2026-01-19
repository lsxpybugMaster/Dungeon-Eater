using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainTitleUI : TextBtnUI
{
    protected override void BindBtnInbtnList(List<Button> btnList)
    {
        BindBtn(0, () =>
        {
            SceneManager.LoadScene("MainGame");
        });

        BindBtn(1, () =>
        {
            Application.Quit();
        });
    }
}
