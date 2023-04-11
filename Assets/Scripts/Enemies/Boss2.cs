using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
{
    protected override float BasicAttackDelay => 2.5f;

    protected override float CircleAttackDelay => 4.5f;

    protected override float Damage => 4f;

    [SerializeField]
    private GameObject targetBullet;

    protected override void Attack()
    {
        base.Attack();

        //BeziarShoot();
    }


    void BeziarShoot()
    {
        Bullet bullet;

        for (int i = 0; i < 7; i++)
        {
            bullet = Instantiate(targetBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

            bullet.GoTarget(Utility.Player.transform.position);
        }
    }
}
