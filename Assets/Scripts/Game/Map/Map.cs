using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] Transform spawnPos;

    public Transform GetSpawnPos()
    {
        return spawnPos;
    }
}
