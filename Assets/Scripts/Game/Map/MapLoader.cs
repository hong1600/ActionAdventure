using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMap
{
    MAP1,
    MAP2,
    BOSSROOM,
}

public class MapLoader : MonoBehaviour
{
    [SerializeField] List<GameObject> mapList = new List<GameObject>();

    GameObject curMap;

    PlayerSpawner playerSpawner;

    bool isLoading;

    private void Awake()
    {
        playerSpawner = GameManager.instance.PlayerSpawner;
    }

    public void LoadMap(EMap _eMap)
    {
        StartCoroutine(StartLoadMap(_eMap, false, default(ECheckPointID), false));
    }

    public void LoadCheckPoint(EMap _emap, ECheckPointID _id)
    {
        StartCoroutine(StartLoadMap(_emap, true, _id, false));
    }

    public IEnumerator LoadRespawn(EMap _map, ECheckPointID _id)
    {
        if (isLoading) yield break;

        isLoading = true;

        yield return StartCoroutine(StartLoadMap(_map, true, _id, true));

        isLoading = false;
    }

    IEnumerator StartLoadMap(EMap _eMap, bool _useCheckPoint, ECheckPointID _id, bool _isRespawn)
    {
        if (curMap != null)
        {
            Destroy(curMap);
            yield return null;
        }

        curMap = Instantiate(mapList[(int)_eMap]);

        Vector3 spawnPos;

        if (_useCheckPoint)
        {
            CheckPoint point = GameManager.instance.CheckPointManager.FindCheckPoint(_id);

            if (point == null) yield break;

            spawnPos = point.SpawnPos;
        }
        else
        {
            Map map = curMap.GetComponent<Map>();

            spawnPos = map.GetSpawnPos().position;
        }

        if (_isRespawn)
        {
            playerSpawner.Spawn(spawnPos);
        }
        else
        {
            playerSpawner.MovePlayer(spawnPos);
        }

        switch (_eMap) 
        {
            case EMap.MAP1:
                AudioManager.instance.PlayBgm(EBgm.MAP1);
                break;
            case EMap.MAP2:
                AudioManager.instance.PlayBgm(EBgm.MAP2);
                break;
        }
    }
}
