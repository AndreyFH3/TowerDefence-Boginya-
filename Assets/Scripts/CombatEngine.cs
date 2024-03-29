public static class CombatEngine
{
    private static float maxDamage = 2.0f;
    private static float midDamage = .5f;
    private static float minDamage = .25f;
    
    public static void DamageObject<T>(this Bullet bullet, T enemy) where T : Health
    {
        if(enemy is MainTowerHealth)
        {
            enemy.GetDamage(bullet.Damage);
            return;
        }

        switch (bullet.GetDamageType)
        {
            case DamageType.Electricity:
                enemy.ElectricityDamage(bullet.Damage);
                break;
            case DamageType.Fire:
                enemy.FireDamage(bullet.Damage);
                break;
            case DamageType.Physical:
                enemy.PhysicalDamage(bullet.Damage);
                break;
            case DamageType.Water:
                enemy.WaterDamage(bullet.Damage);
                break;
        }
    }
    
    private static void ElectricityDamage<T>(this T enemy, int damage) where T : Health
    {
        switch (enemy.GetDamageType)
        {
            case DamageType.Electricity:
                enemy.GetDamage((int)(damage * minDamage));
                break;
            case DamageType.Fire:
                enemy.GetDamage(damage);
                break;
            case DamageType.Physical:
                enemy.GetDamage((int)(damage * midDamage));
                break;
            case DamageType.Water:
                enemy.GetDamage((int)(damage * maxDamage));
                break;
        }
    }
    
    private static void PhysicalDamage<T>(this T enemy, int damage) where T : Health
    {
        switch (enemy.GetDamageType)
        {
            case DamageType.Electricity:
                enemy.GetDamage(damage);
                break;
            case DamageType.Fire:
                enemy.GetDamage(damage);
                break;
            case DamageType.Physical:
                enemy.GetDamage((int)(damage * 1.5f));
                break;
            case DamageType.Water:
                enemy.GetDamage(damage);
                break;
        }
    }

    private static void FireDamage<T>(this T enemy, int damage) where T : Health
    {
        switch (enemy.GetDamageType)
        {
            case DamageType.Electricity:
                enemy.GetDamage((int)(damage * midDamage));
                break;
            case DamageType.Fire:
                enemy.GetDamage((int)(damage * minDamage));
                break;
            case DamageType.Physical:
                enemy.GetDamage(damage);
                break;
            case DamageType.Water:
                enemy.GetDamage((int)(damage * maxDamage));
                break;
        }
    }

    private static void WaterDamage<T>(this T enemy, int damage) where T : Health
    {
        switch (enemy.GetDamageType)
        {
            case DamageType.Electricity:
                enemy.GetDamage(damage * 2);
                break;
            case DamageType.Fire:
                enemy.GetDamage((int)(damage * minDamage));
                break;
            case DamageType.Physical:
                enemy.GetDamage(damage);
                break;
            case DamageType.Water:
                enemy.GetDamage((int)(damage * minDamage));
                break;
        }
    }
}