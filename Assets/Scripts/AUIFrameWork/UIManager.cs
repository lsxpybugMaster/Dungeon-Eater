using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Layer Roots")]
    [SerializeField] Transform globalRoot;
    [SerializeField] Transform pageRoot;
    [SerializeField] Transform popupRoot;
    [SerializeField] Transform overlayRoot;

    private readonly Dictionary<Type, BaseUI> cache = new();

    public T Show<T>(object param = null) where T : BaseUI
    {
        var ui = Get<T>();
        ui.Show(param);
        return ui;
    }

    public void Hide<T>() where T : BaseUI
    {
        if (cache.TryGetValue(typeof(T), out var ui))
            ui.Hide();
    }

    private T Get<T>() where T : BaseUI
    {
        var type = typeof(T);

        if (!cache.TryGetValue(type, out var ui))
        {
            ui = FindObjectOfType<T>(true);
            if (ui == null)
            {
                Debug.LogError($"UI {type.Name} not found in scene.");
                return null;
            }

            cache[type] = ui;
            // SetParent(ui);
        }

        return (T)ui;
    }

    //private void SetParent(BaseUI ui)
    //{
    //    Transform parent = ui.Layer switch
    //    {
    //        UILayer.Global => globalRoot,
    //        UILayer.Page => pageRoot,
    //        UILayer.Popup => popupRoot,
    //        UILayer.Overlay => overlayRoot,
    //        _ => null
    //    };

    //    ui.transform.SetParent(parent, false);
    //}
}
