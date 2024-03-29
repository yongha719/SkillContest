﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private TextMeshProUGUI ScoreText;

    [SerializeField]
    private List<Material> stageSkybox = new List<Material>();

    private uint score;
    public uint Score
    {
        get => score;

        set
        {
            score = value;
            ScoreText.text = $"Score: {score,10}";
        }
    }

    private bool isClear;
    public bool IsClear
    {
        get => isClear;
        set
        {
            isClear = value;
            // 결과창 만들어서 띄우기
            if (isClear)
            {
                OnScorePopup();
            }
        }
    }

    [SerializeField]
    private GameObject ScorePopup;

    private TextMeshProUGUI ScorePopupScore;

    public static int StageNum = 1;
    public static float StageMultiplier => StageNum switch
    {
        2 => 1.3f,
        3 => 1.6f,
        _ => 1f
    };


    public List<BasicEnemy> EnemyList = new List<BasicEnemy>();

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Title")
        {
            StartCoroutine(EScorePlus());

            Score = 0;

            RenderSettings.skybox = stageSkybox[StageNum - 1];
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnManager.Instance.RemoveEnemies();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            Utility.Player.MaxPowerup();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            Utility.Player.InitSkill();
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            Utility.Player.Hp = 100f;
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            Utility.Player.Fuel = 50f;
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            NextStage();
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            SpawnManager.Instance.BossSpawn();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene("Title");
    }

    void OnScorePopup()
    {
        GameObject popup = Instantiate(ScorePopup);
        popup.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() =>
        {
            NextStage();
            Destroy(popup);
        });

        ScorePopupScore = popup.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        ScorePopupScore.text = $"Score : {Score,-10}";
        //ScorePopupTime = popup.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void NextStage()
    {
        if (StageNum != 3)
        {
            SceneManager.LoadScene($"Stage{StageNum + 1}");

            StageNum++;
        }
    }

    public void SlowEnemyAttackDelay()
    {
        foreach (var enemy in EnemyList)
        {
            enemy.SetAttackDelay(enemy.AttackDelay * Utility.EnemyScaledTime);
        }
    }


    IEnumerator EScorePlus()
    {
        var wait = new WaitForSeconds(0.25f);

        while (true)
        {
            Score += 5;
            yield return wait;
        }
    }
}
