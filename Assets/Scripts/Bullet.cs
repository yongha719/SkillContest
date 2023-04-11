using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigid;

    public bool IsShootByPlayer;
    public float Speed;
    public float Damage;

    public bool IsBasicShoot = true;
    private bool IsVisible;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();

        //StartCoroutine(EGoTarget(Utility.Player.transform.position));
    }

    void Update()
    {
        if (IsShootByPlayer == false)
            transform.Translate(Vector3.up * Speed * Utility.EnemyBulletScaledDeltaTime);
        else
            transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    public void GoTarget(Vector3 target)
    {
        print(gameObject.activeSelf);

        StartCoroutine(EGoTarget(target));

        IsBasicShoot = false;
    }

    private IEnumerator EGoTarget(Vector3 targetpos)
    {
        int temp = Random.Range(0, 2) == 0 ? 0 : 1;

        rigid.AddForce(new Vector3(7, 0, 28), ForceMode.Impulse);

        yield return new WaitForSeconds(2f);

        IsBasicShoot = true;

        transform.LookAt(targetpos);
        transform.Rotate(90, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsShootByPlayer && other.TryGetComponent(out BasicEnemy enemy))
        {
            enemy.Hit(Damage * GameManager.StageMultiplier);

            Destroy(gameObject);
        }
        else if (IsShootByPlayer == false && other.TryGetComponent(out Player player))
        {
            player.Hp -= Damage * GameManager.StageMultiplier;

            Destroy(gameObject);
        }
        else if (IsShootByPlayer && other.TryGetComponent(out Boss boss))
        {
            boss.Hit(Damage * GameManager.StageMultiplier);

            Destroy(gameObject);
        }
        else if (IsShootByPlayer && other.TryGetComponent(out Stone stone))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnBecameVisible()
    {
        IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (IsVisible)
            Destroy(gameObject);
    }
}
