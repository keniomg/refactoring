using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _shootingTarget;

    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_shootingDelay);
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            Vector3 shootingDirection = (_shootingTarget.position - transform.position).normalized;
            GameObject newBullet = Instantiate(_bulletPrefab, transform.position + shootingDirection, Quaternion.identity);

            if (newBullet.TryGetComponent(out Rigidbody bulletRigidbody))
            {
                bulletRigidbody.transform.up = shootingDirection;
                bulletRigidbody.velocity = shootingDirection * _bulletSpeed;
            }

            yield return _waitForSeconds;
        }
    }
}