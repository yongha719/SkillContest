using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool IsInvisible;
    public const float MaxHp = 100f;

    [SerializeField]
    private float hp = MaxHp;
    public float Hp
    {
        get
        {
            return hp;
        }

        set
        {
            if (IsInvisible && value < hp)
                return;

            if (value <= 0)
            {
                IsDie = true;
            }



            hp = value;

            if (hp > MaxHp)
                hp = MaxHp;

            hpSlider.value = hp;
        }
    }

    public const float Maxfuel = 50f;

    [SerializeField]
    private float fuel = Maxfuel;
    public float Fuel
    {
        get
        {
            return fuel;
        }

        set
        {
            if (value <= 0)
            {
                IsDie = true;
            }

            fuel = value;

            if (fuel > Maxfuel)
                fuel = Maxfuel;

            fuelSlider.value = fuel;
        }
    }

    [SerializeField]
    private float Speed = 15f;

    private bool CanAttack => IsDie == false && ComingBoss == false;
    public bool IsDie;
    public bool ComingBoss;


    [SerializeField]
    private GameObject playerBullet;

    [SerializeField]
    private Slider hpSlider;
    [SerializeField]
    private Slider fuelSlider;

    [SerializeField]
    private FillImage FixSkillImage;
    [SerializeField]
    private FillImage BoomSkillImage;
    [SerializeField]
    private FillImage SlowEnemyBulletSkillImage;

    [SerializeField]
    private GameObject WarningText;

    [SerializeField]
    AudioClip ShootSound;

    [SerializeField]
    AudioSource AudioSource;

    private float Damage = 6f;
    private WaitForSeconds AttackDelay => new WaitForSeconds(0.15f);
    private WaitKeyDown WaitPressAttackKey => new WaitKeyDown(KeyCode.Z);


    private float HealHp = 15f;
    private float FixSkillcurtime;
    private float FixSkillDelay = 9f;
    private WaitKeyDown WaitPressFixSkillKey => new WaitKeyDown(KeyCode.X);


    private float BoomSkillDamage = 15f;
    private float BoomSkillCurtime;
    private float BoomSkillDelay = 7f;
    private WaitKeyDown WaitPressBoomSkillKey => new WaitKeyDown(KeyCode.C);


    private float SlowScale = 0.5f;
    private float SlowBulletSkillCurtime;
    private float SlowBulletSkillDelay = 10f;
    private WaitForSeconds BackOriginalSpeedDelay => new WaitForSeconds(2.5f);
    private WaitKeyDown WaitPressSlowBulletSkillKey => new WaitKeyDown(KeyCode.LeftShift);

    const float MAX_POWERUP = 4f;
    float powerupCount = 1;

    const float InvisibleTime = 2f;
    float curInvisibleTime = 0;

    private void Awake()
    {
        Utility.Player = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(EBasicAttack());
        StartCoroutine(EFuelDecrease());

        InitSkill();
        SKillsCoroutine();
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")) * Time.deltaTime * Speed, Space.World);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), 0, Mathf.Clamp(transform.position.z, -0.5f, 8f));
    }

    public void MaxPowerup()
    {
        powerupCount = MAX_POWERUP;
    }

    IEnumerator EBasicAttack()
    {
        while (true)
        {
            yield return null;

            if (CanAttack == false)
                continue;


            yield return AttackDelay;
            yield return WaitPressAttackKey;

            // 총알발사
            switch (powerupCount)
            {
                case 1:
                    Instantiate(playerBullet, transform.position, playerBullet.transform.rotation);
                    break;
                case 2:
                    Instantiate(playerBullet, transform.position - Vector3.left * 0.5f, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.right * 0.5f, playerBullet.transform.rotation);
                    break;
                case 3:
                    Instantiate(playerBullet, transform.position, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.left * 0.8f, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.right * 0.8f, playerBullet.transform.rotation);
                    break;
                case 4:
                    Instantiate(playerBullet, transform.position - Vector3.left * 0.4f, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.right * 0.4f, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.left * 1.2f, playerBullet.transform.rotation);
                    Instantiate(playerBullet, transform.position - Vector3.right * 1.2f, playerBullet.transform.rotation);
                    break;
                default:
                    throw new System.Exception("야야 이거 문제있는데?");
            }
            AudioSource.PlayOneShot(ShootSound);
        }
    }

    void SKillsCoroutine()
    {
        StartCoroutine(EFixSkill());
        StartCoroutine(EBoomSkill());
        StartCoroutine(ESlowEnemyBulletSkill());
    }

    public void InitSkill()
    {
        FixSkillcurtime = FixSkillDelay;
        FixSkillImage.StopFill();

        BoomSkillCurtime = BoomSkillDelay;

        SlowBulletSkillCurtime = SlowBulletSkillDelay;
    }

    IEnumerator EFixSkill()
    {
        while (true)
        {
            if (FixSkillcurtime >= FixSkillDelay)
            {
                yield return WaitPressFixSkillKey;
                print("Fix Skill");

                FixSkillImage.StartFill(FixSkillDelay);
                Hp += HealHp * GameManager.StageMultiplier;
                FixSkillcurtime = 0;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    WarningText.SetActive(true);
                    SoundManager.Instance.PlayWarningSound();
                }
                FixSkillcurtime += Time.deltaTime;
            }

            yield return null;
        }
    }

    IEnumerator EBoomSkill()
    {
        while (true)
        {
            if (BoomSkillCurtime >= BoomSkillDelay)
            {
                yield return WaitPressBoomSkillKey;
                print("Boom Skill");

                var hits = Physics.SphereCastAll(transform.position, 25f, Vector3.up);

                foreach (var hit in hits)
                {
                    print(hit.rigidbody.name);
                    if (hit.rigidbody.TryGetComponent(out BasicEnemy enemy))
                    {
                        print("Enemy hit");
                        enemy.Hp -= BoomSkillDamage * GameManager.StageMultiplier;
                    }
                    else if (hit.rigidbody.TryGetComponent(out Bullet bullet) && bullet.IsShootByPlayer == false)
                    {
                        Destroy(bullet.gameObject);
                    }

                }

                BoomSkillImage.StartFill(BoomSkillDelay);
                BoomSkillCurtime = 0;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    WarningText.SetActive(true);
                    SoundManager.Instance.PlayWarningSound();
                }
                BoomSkillCurtime += Time.deltaTime;
            }

            yield return null;
        }
    }

    IEnumerator ESlowEnemyBulletSkill()
    {
        while (true)
        {
            if (SlowBulletSkillCurtime >= SlowBulletSkillDelay)
            {
                yield return WaitPressSlowBulletSkillKey;

                Utility.EnemyBulletScaledTime = SlowScale;
                yield return BackOriginalSpeedDelay;
                Utility.EnemyBulletScaledTime = 1f;

                SlowEnemyBulletSkillImage.StartFill(SlowBulletSkillDelay);

                SlowBulletSkillCurtime = 0f;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    WarningText.SetActive(true);
                    SoundManager.Instance.PlayWarningSound();
                }
                SlowBulletSkillCurtime += Time.deltaTime;
            }

            yield return null;
        }
    }


    public void PowerUp()
    {
        if (powerupCount < MAX_POWERUP)
        {
            // 나중에 이펙트
            Damage += 4f;

            powerupCount++;
        }
    }

    public void Invisible()
    {
        curInvisibleTime = 0;

        if (IsInvisible)
            return;

        IsInvisible = true;

        StartCoroutine(EInvisibleCountDown());
    }

    private IEnumerator EInvisibleCountDown()
    {
        while (true)
        {
            if (curInvisibleTime >= InvisibleTime)
            {
                IsInvisible = false;
                curInvisibleTime = 0;
                yield break;
            }
            else
                curInvisibleTime += Time.deltaTime;

            yield return null;
        }
    }


    IEnumerator EFuelDecrease()
    {
        var wait = new WaitForSeconds(0.7f);

        while (true)
        {
            Fuel -= 0.5f;
            yield return wait;
        }
    }

    private void Reset()
    {
        Speed = 15f;
        hp = MaxHp;


        IsDie = false;
        ComingBoss = false;

        Damage = 6f;
        BoomSkillDamage = 30f;
        SlowScale = 0.5f;
        HealHp = 15f;
    }
}
