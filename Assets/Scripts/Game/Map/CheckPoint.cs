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
    [SerializeField] Vector3 offset;
    [SerializeField] Transform sitPoint;
    Transform spawnPoint;
    bool isResting;

    public CheckPointData CheckPointData => data; 
    public Vector3 SpawnPos => spawnPoint.position;

    public Vector3 interactionOffset { get { return offset; } }
    public EInteractionText interactionText => EInteractionText.REST;

    private void Awake()
    {
        spawnPoint = transform;
    }

    public void Interact() { }

    public void Interact(PlayerManager _player)
    {
        if (isResting) return;

        StartCoroutine(StartRest(_player));
    }

    IEnumerator StartRest(PlayerManager _player)
    {
        isResting = true;

        GameManager.instance.GameState.SetState(EGameState.DONTMOVE);

        Rigidbody2D rigid = _player.PlayerMovement.rigid;

        rigid.velocity = Vector2.zero;
        rigid.position = sitPoint.position;

        _player.PlayerAnimation.ChangeAnim(EPlayerAnim.REST);

        yield return new WaitForSeconds(0.5f);

        _player.PlayerStatus.RestHp();

        _player.GetComponent<HitFlash>().Flash(0.5f);

        GameManager.instance.CheckPointManager.Activate(this);

        while (true)
        {
            float inputX = Input.GetAxisRaw("Horizontal");

            if (inputX != 0) break;

            yield return null;
        }

        _player.PlayerAnimation.ChangeAnim(EPlayerAnim.IDLE);

        GameManager.instance.GameState.SetState(EGameState.PLAY);

        isResting = false;
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
