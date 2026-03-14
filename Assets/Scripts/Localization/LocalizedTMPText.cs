using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LocalizedTMPText : MonoBehaviour
{
    [Header("传入Key, 进行本地化解析")]
    public string key;

    TMP_Text text;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        text.text = LocalizationManager.Instance.Get(key);
    }
}