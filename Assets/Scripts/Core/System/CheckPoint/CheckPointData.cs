using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/CheckPoint")]
public class CheckPointData : ScriptableObject
{
    public ECheckPointID checkPointID;
    public EMap map;
    public string checkPointName;
}