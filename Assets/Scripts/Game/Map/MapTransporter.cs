using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTransporter : MonoBehaviour
{
    FadeUI fade;

    [SerializeField] EMap eMap;

    private void Start()
    {
        fade = UIManager.instance.GamePanel.Fade;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(StartTransport());
    }

    IEnumerator StartTransport()
    {
        yield return fade.FadeIn(fade.FadeImg, 1f);

        GameManager.instance.MapLoader.LoadMap(eMap);
        CameraManager.instance.CameraBounds.ChangeBounds(eMap);

        fade.FadeOut(fade.FadeImg, 1f);
    }
}
