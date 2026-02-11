using System.Collections;
using System.Collections.Generic;
using TMPro;
using UIUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//事先封装好的Unity Button, 包含Button和TextMesh组件的自动获取
public class UButton : MonoBehaviour
{
    [SerializeField] protected Button button;
    [SerializeField] protected TMP_Text tmp_text;

    public string ButtonText
    {
        get { return tmp_text.text; }
        set { tmp_text.text = value;}
    }

    public Button Btn => button;

    /// <summary>
    /// 绑定点击时的函数 , 注意要避免反复绑定相同的函数
    /// </summary>
    /// <param name="a"></param>
    public void AddListenerOnClick(UnityAction a)
    {
        button.onClick.AddListener(a);
    }

    /// <summary>
    /// 禁止按钮的交互功能
    /// </summary>
    public void DisableButton(bool clearAllListeners = true)
    {
        //如果需要, 清空之前的所有监听事件, 避免重复绑定
        if (clearAllListeners)
            button.onClick.RemoveAllListeners();
        button.interactable = false;
    }

    /// <summary>
    /// 重启按钮的交互功能
    /// </summary>
    public void EnableButton()
    {
        button.interactable = true;
    }


    //自动识别组件是否配置,没配置则自动配置,若再没有则报错
    public void Awake()
    {
        if (tmp_text == null)
        {
            tmp_text = GetComponentInChildren<TMP_Text>();
            button = GetComponent<Button>();
            
            if (tmp_text == null)
                Debug.LogError("UButton 没有配置 TMP_Text");
            if (button == null)
                Debug.LogError("UButton 没有配置 Button");
        }
    }
}
