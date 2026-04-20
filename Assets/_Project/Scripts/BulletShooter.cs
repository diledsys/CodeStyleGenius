using System.Collections;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootInterval = 1f;

    private Coroutine _shootingCoroutine;

    private void OnEnable()
    {
        _shootingCoroutine = StartCoroutine(ShootingRoutine());
    }

    private void OnDisable()
    {
        if (_shootingCoroutine != null)
            StopCoroutine(_shootingCoroutine);
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            Shoot();

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    private void Shoot()
    {
        if (_bulletPrefab == null)
        {
            Debug.LogError($"{nameof(BulletShooter)}: Bullet prefab is not assigned.", this);
            return;
        }

        if (_target == null)
        {
            Debug.LogError($"{nameof(BulletShooter)}: Target is not assigned.", this);
            return;
        }

        Vector3 direction = ( _target.position - transform.position ).normalized;

        Rigidbody bullet = Instantiate(
            _bulletPrefab,
            transform.position + direction,
            Quaternion.identity);

        bullet.transform.up = direction;
        bullet.linearVelocity = direction * _bulletSpeed;
    }
}