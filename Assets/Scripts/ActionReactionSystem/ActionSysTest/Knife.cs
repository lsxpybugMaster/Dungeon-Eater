﻿using ActionSystemTest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystemTest
{
    public class Knife : MonoBehaviour
    {
        private void OnEnable()
        {
            ActionSystem.SubscribeReaction<DrawCardGA>(DrawCardReaction, ReactionTiming.POST);
        }

        private void OnDisable()
        {
            ActionSystem.UnsubscribeReaction<DrawCardGA>(DrawCardReaction, ReactionTiming.POST);
        }

        private void DrawCardReaction(DrawCardGA drawCardGA)
        {
            DealDamageGA dealDamageGA = new(3);
            Debug.Log("执行AddReaction");
            ActionSystem.Instance.AddReaction(dealDamageGA);
        }
    }
}