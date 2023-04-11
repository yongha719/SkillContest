using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : BasicEnemy
{
    public override float AttackDelay => 1.4f;
    protected override float Damage => 4f;

    protected override float Speed => 7f;

    protected override IEnumerator EBasicAttack()
    {
        Bullet bullet = null;

        while (true)
        {
            bullet = Instantiate(Bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

            bullet.transform.LookAt(Utility.Player.transform);
            bullet.transform.Rotate(90, 0, 0);
            bullet.Damage = Damage;

            yield return WaitAttack;
        }
    }

    private void OnDestroy()
    {
        if (Random.Range(0f, 10f) <= 1f)
            SpawnManager.Instance.ItemSpawn(transform.position);

        GameManager.Instance.Score += (uint)Mathf.Round(35 * GameManager.StageMultiplier);
    }
}
