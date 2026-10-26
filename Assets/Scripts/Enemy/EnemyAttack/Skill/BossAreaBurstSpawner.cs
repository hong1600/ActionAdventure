using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAreaBurstSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] GameObject warningPrefab;
    [SerializeField] GameObject explosionPrefab;

    GameObject[] warningObjs;

    public int PointCount => spawnPoints.Length;

    private void Awake()
    {
        warningObjs = new GameObject[PointCount];
    }

    public void SpawnWarning(int _index)
    {
        if (_index < 0 || _index >= spawnPoints.Length)
        {
            return;
        }

        warningObjs[_index] = Instantiate(warningPrefab, spawnPoints[_index].position, Quaternion.identity);
    }

    public void Explode(int _index)
    {
        if (_index < 0 || _index >= spawnPoints.Length)
        {
            return;
        }

        if (warningObjs[_index] != null)
        {
            Destroy(warningObjs[_index]);
            warningObjs[_index] = null;
        }

        GameObject explosion = Instantiate(explosionPrefab, spawnPoints[_index].position, Quaternion.identity);

        Destroy(explosion, 0.5f);
    }

    public void Clear()
    {
        for (int i = 0; i < warningObjs.Length; i++)
        {
            if (warningObjs[i] == null)
            {
                continue;
            }

            Destroy(warningObjs[i]);
            warningObjs[i] = null;
        }
    }
}
