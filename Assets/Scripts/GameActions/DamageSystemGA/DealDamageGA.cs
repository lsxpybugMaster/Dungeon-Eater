
//必须把处理伤害的GameAction分开成一个单独的GA，否则在DealAttackGA里修改伤害会导致动画和伤害分开，无法同时修改
public class DealDamageGA : GameAction
{
    public CombatantView DamageTaker { get; private set; }
    public int Damage { get; private set; }

    public DealDamageGA(CombatantView damageTaker, int damage)
    {
        DamageTaker = damageTaker;
        Damage = damage;
    }
}
