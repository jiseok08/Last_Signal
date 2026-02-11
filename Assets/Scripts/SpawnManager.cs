using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> positions;
    [SerializeField] List<Unit> unitPrefabs;
    [SerializeField] List<Unit> bossPrefabs;

    [SerializeField] float baseCooldown = 0.9f;
    [SerializeField] float createCooldown = 0.9f;
    [SerializeField] int waveCreateCount = 10;
    [SerializeField] int maxCreateCount = 30;

    [SerializeField] public Dictionary<Unit, List<Unit>> pools = new Dictionary<Unit, List<Unit>>();
    [SerializeField] public List<Unit> unitList = new List<Unit>();

    [SerializeField] private int aliveCount = 0;
    private int random = 0;

    private int bossCount = 0;

    Coroutine spawnCoroutine;

    private void Awake()
    {
        UnitListFull();

        if (spawnCoroutine == null)
        {
            spawnCoroutine = StartCoroutine(CreateRoutine());
        }
    }
            


    void UnitListFull()
    {
        aliveCount = waveCreateCount;

        while(unitList.Count < waveCreateCount)
        {
            if (GameManager.Instance.Wave.wave >= 6)
            {
                random = Random.Range(0, 7);

                if (random == 6)
                {
                    GetPool(unitPrefabs[2]);

                    continue;
                }
            }

            if (GameManager.Instance.Wave.wave >= 3)
            {
                random = Random.Range(0, 3);

                if (random == 2)
                {
                    GetPool(unitPrefabs[1]);

                    continue;
                }
            }
            else
            {
                GetPool(unitPrefabs[0]);
            }
        }
    }

    void GetPool(Unit prefab)
    {
        if (pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new List<Unit>();
        }

        Unit unit = null;

        if (pools[prefab].Count == 0)
        {
            unit = Instantiate(prefab, positions[Random.Range(0, positions.Count)].position, Quaternion.identity, transform);
        }
        else
        {
            unit = pools[prefab][pools[prefab].Count - 1];

            Debug.Log(unit);
        }

        unit.gameObject.SetActive(false);

        unit.ParentPrefab = prefab;

        unitList.Add(unit);

        pools[prefab].Remove(unit);
    }

    public void Release(Unit prefab, Unit unit)
    {
        aliveCount--;

        unit.transform.position = positions[Random.Range(0, positions.Count - 1)].position;

        unit.gameObject.SetActive(false);

        if (pools.ContainsKey(prefab) == false)
        {
            pools.Add(prefab, new List<Unit>());
        }

        pools[prefab].Add(unit);

        Debug.Log(aliveCount);
    }

    IEnumerator CreateRoutine()
    {
        while (true)
        {
            for (int i = 0; i < waveCreateCount; i++)
            {
                unitList[0].gameObject.SetActive(true);

                unitList.RemoveAt(0);

                Debug.Log("Create");

                yield return CoroutineCache.WaitForSeconds(createCooldown);
            }

            yield return new WaitUntil(() => aliveCount == 0);

            GameManager.Instance.Wave.WaveUp();

            if (GameManager.Instance.Wave.BossWave())
            {
                // unitList.Add(bossPrefabs[bossCount++ % bossPrefabs.Count]);

                // if (waveCreateCount < maxCreateCount)
                // {
                //     waveCreateCount = Mathf.Min(maxCreateCount, waveCreateCount + 10);
                // }
            }

            UnitListFull();

            Debug.Log(waveCreateCount);

            createCooldown = GameManager.Instance.Wave.wave * 0.03f;
        }
    }
}
