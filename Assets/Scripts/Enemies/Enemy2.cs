using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : BasicEnemy
{
    public override float AttackDelay => 1.3f;

    protected override float Speed => 7f;

    protected override float Damage => 4f;

    Vector3[] bulletPos = new Vector3[] { new Vector3(0.3f, 0, 0), new Vector3(-0.3f, 0, 0) };

    protected override IEnumerator EBasicAttack()
    {
        Bullet bullet = null;

        while (true)
        {
            for (int i = 0; i < 2; i++)
            {
                bullet = Instantiate(Bullet, transform.position + bulletPos[i], Quaternion.identity).GetComponent<Bullet>();
                bullet.transform.LookAt(Utility.Player.transform);
                bullet.transform.Rotate(90, 0, 0);
                bullet.Damage = Damage;
            }

            yield return WaitAttack;
        }
    }


    protected override void Update()
    {
        base.Update();
    }

    private void OnDestroy()
    {
        if (Random.Range(0f, 10f) <= 1.8f)
            SpawnManager.Instance.ItemSpawn(transform.position);

        GameManager.Instance.Score += (uint)Mathf.Round(50 * GameManager.StageMultiplier);
    }
}
