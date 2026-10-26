using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECheckPointID
{
    NONE,
    MAP1_BENCH1,
    MAP2_BENCH1,
    MAP2_BENCH2,
    MAP2_BENCH3,
    MAP2_BENCH4,
}

public class CheckPoint : MonoBehaviour, IInteractable
{
    [SerializeField] CheckPointData data;
    [SerializeField] Transform spawnPoint;

    public CheckPointData CheckPointData => data; 
    public Vector3 SpawnPos => spawnPoint.position;

    public Vector3 interactionOffset { get { return offset; } }
    public EInteractionText interactionText => EInteractionText.REST;

    [SerializeField] Vector3 offset;

    public void Interact()
    {
        GameManager.instance.CheckPointManager.Activate(this);
    }

    private void OnEnable()
    {
        GameManager.instance.CheckPointManager.Register(this);
    }

    private void OnDisable()
    {
        GameManager.instance.CheckPointManager.Unregister(this);
    }
}
