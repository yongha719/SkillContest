using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlowItem : Item
{
    protected override void ItemEffect(Player player)
    {
        StartCoroutine(ESlowEnemyItem());
    }

    IEnumerator ESlowEnemyItem()
    {
        Utility.EnemyScaledTime = Utility.EnemyBulletScaledTime = 0.6f;
        GameManager.Instance.SlowEnemyAttackDelay();

        yield return new WaitForSeconds(6f);

        Utility.EnemyScaledTime = Utility.EnemyBulletScaledTime = 1f;
        GameManager.Instance.SlowEnemyAttackDelay();
    }
}
