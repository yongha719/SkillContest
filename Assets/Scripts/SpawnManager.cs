using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField]
    private GameObject Stone;

    [SerializeField]
    private List<GameObject> Boss = new List<GameObject>();

    public List<GameObject> Enemies = new List<GameObject>();

    [SerializeField]
    private List<GameObject> Items = new List<GameObject>();

    Coroutine EnemySpawnCoroutine;

    bool AlreadyBossSpawn;

    IEnumerator Start()
    {
        EnemySpawn();
        StartCoroutine(EStoneSpawn());

        yield return new WaitForSeconds(90f);

        if (AlreadyBossSpawn)
            yield break;
        BossSpawn();
    }


    public void RemoveEnemies()
    {
        for(int i =0; i< Enemies.Count; i++)
        {
            if (Enemies[i] != null)
            {
                Destroy(Enemies[i]);
            }
        }
    }

    public void BossSpawn()
    {
        AlreadyBossSpawn = true;
        StopCoroutine(EnemySpawnCoroutine);

        Instantiate(Boss[GameManager.StageNum - 1], new Vector3(0, 0, 30f), Quaternion.Euler(90, 0, 0));
    }

    IEnumerator EStoneSpawn()
    {
        var wait = new WaitForSeconds(6f);

        while (true)
        {
            GameObject stone = Instantiate(Stone, GetRandomStoneSpawnPos(), Quaternion.identity);

            stone.transform.LookAt(Utility.Player.transform);
            stone.transform.Rotate(90, 0, 0);
            stone.transform.localScale = Vector3.one * Random.Range(1.5f, 3f);

            yield return wait;
        }
    }

    private Vector3 GetRandomStoneSpawnPos()
    {
        int temp = Random.Range(0, 3);

        switch (temp)
        {
            case 0:
                return new Vector3(Random.Range(-25f, 25f), 0, 30f);
            case 1:
                return new Vector3(-25f, 0, Random.Range(20f, 30f));
            case 2:
                return new Vector3(-25f, 0, Random.Range(20f, 30f));
        }

        return Vector3.zero;
    }

    public void ItemSpawn(Vector3 pos)
    {
        float temp = Random.Range(0, 100f);

        GameObject item = null;

        if (temp <= 15f)
            item = Items[0];
        else if (temp < 25f)
            item = Items[1];
        else if (temp <= 45f)
            item = Items[2];
        else if (temp <= 75f)
            item = Items[3];
        else
            item = Items[4];

        Instantiate(item, pos, item.transform.rotation);
    }

    public void EnemySpawn() => EnemySpawnCoroutine = StartCoroutine(EEnemySpawn());
    IEnumerator EEnemySpawn()
    {
        yield return new WaitForSeconds(4f);

        WaitForSeconds spawnDelay = GetSpawnDelay(GameManager.StageNum);

        while (GameManager.Instance.IsClear == false || Utility.Player.IsDie == false)
        {
            GameManager.Instance.EnemyList.Add(InstantiateEnemy(GameManager.StageNum).GetComponent<BasicEnemy>());

            yield return spawnDelay;
        }
    }

    public WaitForSeconds GetSpawnDelay(int StageNum) => StageNum switch
    {
        1 => new WaitForSeconds(1.4f),
        2 => new WaitForSeconds(1.1f),
        3 => new WaitForSeconds(0.8f),
        _ => throw new System.Exception()
    };

    public GameObject InstantiateEnemy(int StageNum)
    {
        GameObject Enemy = null;

        int temp = Random.Range(0, 20);

        switch (StageNum)
        {
            case 1:
                Enemy = Enemies[(temp <= 14) ? 0 : 1];
                break;
            case 2:
                if (temp <= 10)
                    Enemy = Enemies[0];
                else if (temp <= 17)
                    Enemy = Enemies[1];
                else
                    Enemy = Enemies[2];
                break;
            case 3:
                if (temp <= 6)
                    Enemy = Enemies[0];
                else if (temp <= 12)
                    Enemy = Enemies[1];
                else
                    Enemy = Enemies[2];
                break;
        }


        return Instantiate(Enemy, GetObjectSpawnPos(), Enemy.transform.rotation);
    }

    Vector3 GetObjectSpawnPos() => new Vector3(Random.Range(-10f, 10f), 0, 35);

    public void ItemSpawn()
    {

    }
}
