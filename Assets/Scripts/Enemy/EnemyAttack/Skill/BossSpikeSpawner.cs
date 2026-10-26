using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpikeSpawner : MonoBehaviour
{
    [SerializeField] BossEnemy boss;
    int activeSpikeCount;

    [SerializeField] Transform[] spawnPoints;

    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject waitEffect;

    [SerializeField] float waitTime = 1.5f;

    public bool IsSpike { get; private set; }

    public void SpawnSpike()
    {
        StartCoroutine(StartSpawnSpike());
    }

    IEnumerator StartSpawnSpike()
    {
        SpawnObj(waitEffect);

        yield return new WaitForSeconds(waitTime);

        activeSpikeCount = spawnPoints.Length;
        SpawnSpikeObj();
    }

    private void SpawnObj(GameObject _prefab)
    {
        for(int i = 0; i < spawnPoints.Length; i++) 
        {
            Transform spawnPoint = spawnPoints[i];

            GameObject go = Instantiate(_prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void SpawnSpikeObj()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform point = spawnPoints[i];

            GameObject go = Instantiate(spikePrefab, point.position, point.rotation);

            Spike spike = go.GetComponent<Spike>();
            spike.SetSpawner(this);
        }
    }

    public void OnSpikeFinished()
    {
        activeSpikeCount--;

        if (activeSpikeCount <= 0)
            boss.SetSpiking(false);
    }
}
