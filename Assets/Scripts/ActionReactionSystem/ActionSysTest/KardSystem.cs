using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystemTest {
    public class KardSystem : MonoBehaviour
    {
        [SerializeField] private Kard cardPrefab;
        [SerializeField] private Transform spawn;
        [SerializeField] private Transform hand;

        private void OnEnable()
        {
            Debug.Log("KardSystem注册DrawCardPerformer协程");
            ActionSystem.AttachPerformer<DrawCardGA>(DrawCardPerformer);
        }

        private void OnDisable()
        {
            ActionSystem.DetachPerformer<DrawCardGA>();
        }

        private IEnumerator DrawCardPerformer(DrawCardGA drawCardGA)
        {
            Debug.Log("执行注册的协程");
            Kard card = Instantiate(cardPrefab, spawn.position, Quaternion.identity);
            Tween tween = card.transform.DOMove(hand.position, 0.5f);
            yield return tween.WaitForCompletion();
        }
    }
}
