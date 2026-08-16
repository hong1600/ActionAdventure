using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CameraShake cameraShake;
    [SerializeField] CameraBounds cameraBounds;

    public CameraShake CameraShake => cameraShake;
    public CameraBounds CameraBounds => cameraBounds;
}
