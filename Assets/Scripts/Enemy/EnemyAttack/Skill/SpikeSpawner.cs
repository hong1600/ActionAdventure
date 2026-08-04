using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;

    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject waitEffect;

    private void SpawnObj(GameObject _prefab)
    {
        for(int i = 0; i < spawnPoints.Length; i++) 
        {
            Transform spawnPoint = spawnPoints[i];

            GameObject go = Instantiate(_prefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    public void SpawnSpike()
    {
        SpawnObj(spikePrefab);
        SpawnObj(waitEffect);
    }
}
