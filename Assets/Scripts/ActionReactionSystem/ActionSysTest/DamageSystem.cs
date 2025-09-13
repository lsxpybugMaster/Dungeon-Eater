using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystemTest
{
    public class DamageSystem : MonoBehaviour
    {
        [SerializeField] private Knife knife;
        [SerializeField] private GameObject health;

        private void OnEnable()
        {
            Debug.Log("DamageSystem注册DealDamagePerformer");
            ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<DealDamageGA>();   
        }

        private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
        {
            int damageAmount = dealDamageGA.Amount;
            Vector2 knifeStart = knife.transform.position;
            Tween tween = knife.transform.DOMove(health.transform.position, 0.25f);
            yield return tween.WaitForCompletion();
            knife.transform.DOMove(knifeStart, 0.5f);
        }
    }
}