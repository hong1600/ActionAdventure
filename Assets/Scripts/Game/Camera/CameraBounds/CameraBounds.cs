using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] CinemachineConfiner2D confiner;

    [SerializeField] List<PolygonCollider2D> coll = new List<PolygonCollider2D>();

    public void ChangeBounds(EMap _map)
    {
        confiner.m_BoundingShape2D = coll[(int)_map];
    }

}
