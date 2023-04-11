using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemy : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get => hp;

        set
        {
            if (value <= 0)
            {
                if (SpawnManager.Instance.Enemies.Contains(gameObject))
                    SpawnManager.Instance.Enemies.Remove(gameObject);

                Destroy(gameObject);
            }

            hp = value;
        }
    }

    [SerializeField]
    protected GameObject Bullet;

    private bool IsVisible;

    private SpriteRenderer SpriteRenderer;

    protected abstract float Speed { get; }
    protected abstract float Damage { get; }

    public abstract float AttackDelay { get; }
    protected WaitForSeconds WaitAttack;

    protected virtual void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        WaitAttack = new WaitForSeconds(AttackDelay);

        Attack();
    }

    protected virtual void Update()
    {
        transform.Translate(Vector3.down * Utility.EnemyScaledDeltaTime * Speed);
    }

    public void Hit(float damage)
    {
        Hp -= damage;

        StartCoroutine(EHit());
    }

    private IEnumerator EHit()
    {
        SpriteRenderer.color = new Color(0.5f, 0.5f, 0.5f);
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.white;
    }

    public void SetAttackDelay(float time)
    {
        WaitAttack = new WaitForSeconds(time);
    }

    protected virtual void Attack()
    {
        StartCoroutine(EBasicAttack());
    }

    protected abstract IEnumerator EBasicAttack();


    private void OnBecameVisible()
    {
        IsVisible = true;
    }

    private void OnBecameInvisible()
    {
        if (IsVisible)
        {
            if (SpawnManager.Instance.Enemies.Contains(gameObject))
                SpawnManager.Instance.Enemies.Remove(gameObject);

            Destroy(gameObject);
        }
    }
}
