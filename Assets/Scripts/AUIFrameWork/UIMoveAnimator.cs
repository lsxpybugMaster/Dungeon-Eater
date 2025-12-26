using UnityEngine;
using DG.Tweening;
using System;

public class UIMoveAnimator : MonoBehaviour
{
    [SerializeField] RectTransform target;
    [SerializeField] float duration = 0.25f;
    [SerializeField] Ease ease = Ease.OutCubic;

    RectTransform rect;
    Vector2 origin;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        origin = rect.anchoredPosition;
    }

    public void PlayEnter()
    {
        rect.DOAnchorPos(target.anchoredPosition, duration).SetEase(ease);
    }

    public void PlayExit(Action onComplete)
    {
        rect.DOAnchorPos(origin, duration)
            .SetEase(ease)
            .OnComplete(() => onComplete?.Invoke());
    }
}
