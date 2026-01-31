using TMPro;
using UnityEngine;

//一些最基本的UI类,只用于增加代码复用
namespace UIUtils
{
    //只含有一个Text,可配置/自动识别
    public class UWithText : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmp_text;

        public string T {
            get { 
                return tmp_text.text;
            }
            set { 
                tmp_text.text = value;
            }
        }

        //自动识别组件是否配置,没配置则自动配置,若再没有则报错
        private void Awake()
        {
            if (tmp_text == null)
            {
                tmp_text = GetComponent<TMP_Text>();
                if (tmp_text == null)
                {
                    Debug.LogError("UWithText 没有配置 TMP_Text");
                }
            }
        }
    }
}
