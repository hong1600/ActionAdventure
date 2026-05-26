using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMap
{
    MAP1,
    MAP2,
}

public class MapLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> mapList = new List<GameObject>();

    GameObject curMap;

    PlayerSpawner playerSpawner;

    private void Awake()
    {
        playerSpawner = GameManager.instance.PlayerSpawner;
    }

    public void LoadMap(EMap _eMap)
    {
        StartCoroutine(StartLoadMap(_eMap));
    }

    IEnumerator StartLoadMap(EMap _eMap)
    {
        if (curMap != null)
        {
            Destroy(curMap);

            yield return null;
        }

        curMap = Instantiate(mapList[(int)_eMap]);

        Map map = curMap.GetComponent<Map>();

        Vector3 spawnPos = map.GetSpawnPos().position;

        playerSpawner.Spawn(spawnPos);
    }
}
