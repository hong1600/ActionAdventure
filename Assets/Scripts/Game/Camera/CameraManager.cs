using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CameraShake cameraShake;
    public CameraShake CameraShake { get { return cameraShake; } }


}
