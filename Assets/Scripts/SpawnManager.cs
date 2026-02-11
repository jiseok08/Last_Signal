using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Transform> positions;
    [SerializeField] List<Unit> unitPrefabs;

    [SerializeField] float baseCooldown = 0.9f;
    [SerializeField] int waveCreateCount = 30;

    public Dictionary<Unit, List<Unit>> pools = new Dictionary<Unit, List<Unit>>();
    public List<Unit> unitList = new List<Unit>();

    private int aliveCount = 0;
    private int random = 0;

    private void Awake()
    {
        UnitListFull();

    }


    void UnitListFull()
    {
        for (int i = 0; i < waveCreateCount; i++)
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

    bool ActiveChack(List<Unit> list)
    {
        for (int i = 0; i == list.Count; i++)
        {
            if (list[i].gameObject.activeSelf == false)
            {
                return false;
            }
        }

        return true;
    }

    void GetPool(Unit prefab)
    {
        if (pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new List<Unit>();
        }

        Unit unit = null;

        if (ActiveChack(pools[prefab]))
        {
            unit = Instantiate(prefab, positions[Random.Range(0, positions.Count)]);

            pools[prefab].Add(unit);
        }

        unitList.Add(pools[prefab][pools[prefab].Count - 1]);
    }

    IEnumerator CreateRoutine()
    {
        while (true)
        {
            for (int i = 0; i < waveCreateCount; i++)
            {


                yield return CoroutineCache.WaitForSeconds(baseCooldown);
            }

            yield return new WaitUntil(() => aliveCount == 0);

            GameManager.Instance.Wave.WaveUp();
        }
    }
}
