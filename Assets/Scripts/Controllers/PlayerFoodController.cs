using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodController : PlayerItemController<FoodData>
{
    public PlayerFoodController(HeroState heroState) : base(heroState)
    {
    }

    public override int Size => heroState.FoodsSize;

    public override void AddToPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void RemoveFromPlayer()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateItems()
    {
        throw new System.NotImplementedException();
    }
}
