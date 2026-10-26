using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    MapLoader mapLoader;

    [SerializeField] CheckPointData[] checkPointDatas;

    HashSet<ECheckPointID> activatedIDs = new HashSet<ECheckPointID>();

    List<CheckPoint> curCheckPointList = new List<CheckPoint>();

    ECheckPointID lastCheckPoint;

    private void Start()
    {
        mapLoader = GameManager.instance.MapLoader;
    }

    public void Activate(CheckPoint _checkPoint)
    {
        ECheckPointID id = _checkPoint.CheckPointData.checkPointID;

        activatedIDs.Add(id);

        lastCheckPoint = id;
    }

    public void Register(CheckPoint _checkPoint)
    {
        if (!curCheckPointList.Contains(_checkPoint))
        {
            curCheckPointList.Add(_checkPoint);
        }
    }

    public void Unregister(CheckPoint _checkPoint)
    {
        curCheckPointList.Remove(_checkPoint);
    }

    public CheckPointData GetData(ECheckPointID _id)
    {
        for (int i = 0; i < checkPointDatas.Length; i++)
        {
            if (checkPointDatas[i].checkPointID == _id)
                return checkPointDatas[i];
        }

        return null;
    }

    public CheckPoint FindCheckPoint(ECheckPointID _id)
    {
        for (int i = 0; i < curCheckPointList.Count; i++)
        {
            CheckPoint point = curCheckPointList[i];

            if (point.CheckPointData.checkPointID == _id)
                return point;
        }

        return null;
    }

    public bool IsActivated(ECheckPointID _id)
    {
        return activatedIDs.Contains(_id);
    }

    public void MoveToCheckPoint(ECheckPointID _id)
    {
        if (!activatedIDs.Contains(_id)) return;

        CheckPointData data = GetData(_id);

        if (data == null) return;

        mapLoader.LoadCheckPoint(data.map, _id);
    }

    public IEnumerator Respawn()
    {
        if (lastCheckPoint == ECheckPointID.NONE) yield break;

        CheckPointData data = GetData(lastCheckPoint);

        if (data == null) yield break;

        yield return mapLoader.LoadRespawn(data.map, lastCheckPoint);
    }
}
