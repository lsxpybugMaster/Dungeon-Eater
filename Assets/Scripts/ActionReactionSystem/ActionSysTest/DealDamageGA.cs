using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionSystemTest {

    public class DealDamageGA : GameAction
    {
        public int Amount;

        //GameAction是纯C#类,需要声明构造函数
        public DealDamageGA(int amount)
        {
            Amount = amount;
        }
    }
}
