using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileShooter : MonoBehaviour
{
    [SerializeField] BossProjectile projectilePrefab;

    [Header("Pattern")]
    [SerializeField] int shotCount = 7;
    [SerializeField] float startAngle = -45f;
    [SerializeField] float endAngle = 45f;
    [SerializeField] float shotInterval = 0.08f;
    [SerializeField] float projectileSpeed = 5f;

    [SerializeField] Transform fireTrs;

    bool isShooting;

    public bool IsShooting => isShooting;

    public void Sweep(float _dirX)
    {
        if (isShooting) return;

        StartCoroutine(StartShot(_dirX));
    }

    IEnumerator StartShot(float _dirX)
    {
        isShooting = true;

        float angleStep = 0f;

        if(shotCount > 1) 
        {
            angleStep = (endAngle - startAngle) / (shotCount - 1);
        }

        for (int i = 0; i < shotCount; i++) 
        {
            float angle = startAngle + angleStep * i;

            if (_dirX < 0f)
            {
                angle = 180f - angle;
            }

            Shot(angle);

            yield return new WaitForSeconds(shotInterval);
        }

        isShooting = false;
    }

    private void Shot(float _angle)
    {
        float radian = _angle * Mathf.Deg2Rad;

        Vector2 dir = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));

        BossProjectile projectile = Instantiate(projectilePrefab, fireTrs.position, Quaternion.identity);

        projectile.SetDirection(dir, projectileSpeed);
    }
}
