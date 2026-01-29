using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoAttackSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<BoostMultiAttackGA>(BoostMulyiAttackPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<BoostMultiAttackGA>();
    }

    private IEnumerator BoostMulyiAttackPerformer(BoostMultiAttackGA ga)
    {
        ga.Caster.M.Contexts.MultiAtk += ga.add;
        yield return null;
    }
}
