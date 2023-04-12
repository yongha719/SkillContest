using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    [SerializeField]
    protected float hp;
    public float Hp
    {
        get => hp;

        set
        {
            if (value <= 0 && GameManager.Instance.IsClear == false)
            {
                GameManager.Instance.IsClear = true;
            }

            hp = value;
        }
    }

    [SerializeField]
    protected GameObject BasicBullet;

    [SerializeField]
    protected GameObject CircleBullet;

    private SpriteRenderer SpriteRenderer;
    private Color OrignalColor;
    private float H;
    private float S;
    private float V;

    protected abstract float Damage { get; }
    protected abstract float BasicAttackDelay { get; }
    protected WaitForSeconds WaitBasicAttack;
    protected abstract float CircleAttackDelay { get; }
    protected WaitForSeconds WaitCircleAttack;

    protected virtual void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        WaitBasicAttack = new WaitForSeconds(BasicAttackDelay);
        WaitCircleAttack = new WaitForSeconds(CircleAttackDelay);

        Attack();

        Move();

        Color.RGBToHSV(SpriteRenderer.color, out H, out S, out V);
    }

    private void Move()
    {
        StartCoroutine(EMove());
    }

    private IEnumerator EMove()
    {
        while (transform.position.z >= 23f)
        {
            transform.Translate(Vector3.down * 4f * Time.deltaTime);
            yield return null;
        }
    }

    protected virtual void Attack()
    {
        StartCoroutine(EBasicAttack());
        StartCoroutine(ECircleAttack());
    }


    protected List<GameObject> CircleBullets = new List<GameObject>();

    protected IEnumerator EBasicAttack()
    {
        Bullet bullet = null;

        while (true)
        {
            bullet = Instantiate(BasicBullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

            bullet.transform.LookAt(Utility.Player.transform);
            bullet.transform.Rotate(90, 0, 0);
            bullet.Damage = Damage;

            yield return WaitBasicAttack;
        }
    }

    protected IEnumerator ECircleAttack()
    {
        while (true)
        {
            CircleAttack();

            yield return WaitCircleAttack;
        }
    }

    void CircleAttack()
    {
        Bullet bullet = null;

        for (int i = 0; i < 360; i += 13)
        {
            bullet = Instantiate(CircleBullet, transform.position, Quaternion.Euler(90, 0, i)).GetComponent<Bullet>();
            bullet.Damage = Damage;

            CircleBullets.Add(bullet.gameObject);
        }

        if (Random.Range(0, 4) == 0)
        {
            StartCoroutine(ECircleAttackGoToPlayer());
        }
    }

    private IEnumerator ECircleAttackGoToPlayer()
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < CircleBullets.Count; i++)
        {
            if (CircleBullets[i] != null)
            {
                CircleBullets[i].transform.LookAt(Utility.Player.transform);
                CircleBullets[i].transform.Rotate(90, 0, 0);
            }
        }
    }

    public void Hit(float damage)
    {
        Hp -= damage;

        StartCoroutine(EHit());
    }

    private IEnumerator EHit()
    {
        SpriteRenderer.color = Color.HSVToRGB(H, S, V / 2);
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer.color = Color.HSVToRGB(H, S, V);
    }
}
