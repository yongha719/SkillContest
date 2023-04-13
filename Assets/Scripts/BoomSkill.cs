using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomSkill : MonoBehaviour
{
    private float BoomSkillDamage = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BasicEnemy enemy))
        {
            print("Enemy hit");
            enemy.Hp -= BoomSkillDamage * GameManager.StageMultiplier;
        }
        else if (other.TryGetComponent(out Bullet bullet) && bullet.IsShootByPlayer == false)
        {
            Destroy(bullet.gameObject);
        }
    }
}
