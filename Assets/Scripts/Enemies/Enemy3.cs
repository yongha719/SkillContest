using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : BasicEnemy
{
    public override float AttackDelay => 4f;

    protected override float Speed => 5f;

    protected override float Damage => 5f;

    protected override IEnumerator EBasicAttack()
    {
        while (true)
        {
            CircleAttack();

            yield return WaitAttack;
        }
    }

    void CircleAttack()
    {
        for(int i = 0; i < 360; i += 13)
        {
            Instantiate(Bullet, transform.position, Quaternion.Euler(90, 0, i));
        }
    }

}
