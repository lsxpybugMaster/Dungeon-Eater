using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Effect�йص�Performerע�����
/// </summary>
public class EffectSystem : MonoBehaviour
{
    void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }

    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffectGA)
    {
        GameAction effectAction = performEffectGA.Effect.GetGameAction();
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
}
