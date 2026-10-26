using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public event Action onSpawnEvent;

    [SerializeField] GameObject playerPrefab;

    [SerializeField] CinemachineVirtualCamera virtualCam;

    public GameObject playerObj { get; private set; }

    public void Spawn(Vector3 _pos)
    {
        if (playerObj != null) return;

        SpawnPlayer(_pos);
    }

    public void Respawn(Vector3 _pos)
    {
        if (playerObj != null)
        {
            Destroy(playerObj);
            playerObj = null;
        }

        SpawnPlayer(_pos);
    }

    public void MovePlayer(Vector3 _pos)
    {
        if (playerObj == null)
        {
            Spawn(_pos);
            return;
        }

        playerObj.transform.position = _pos;
    }

    private void SpawnPlayer(Vector3 _spawnPos)
    {
        GameObject go = Instantiate(playerPrefab, _spawnPos, Quaternion.identity); 

        playerObj = go;

        onSpawnEvent?.Invoke();

        virtualCam.m_Follow = playerObj.transform;
        virtualCam.m_LookAt = playerObj.transform;
    }
}
