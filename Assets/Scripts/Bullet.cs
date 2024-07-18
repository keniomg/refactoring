using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _currentLifetime;
    private float _bulletLifetime;
    private float _bulletSpeed;
    private Vector3 _bulletDirection;
    private WaitForSeconds _waitForSeconds;

    public event Action<Bullet> BulletLifetimeOver;

    private void Start()
    {
        StartCoroutine(LifetimeCountDown());
    }

    private void OnEnable()
    {
        _currentLifetime = 0;
        StartCoroutine(LifetimeCountDown());
    }

    private void Update()
    {
        MoveBullet();
    }

    public void InitializeLifetimeValue(float lifetime)
    {
        _bulletLifetime = lifetime;
    }

    public void InitializeBulletDirection(Vector3 bulletDirection)
    {
        _bulletDirection = bulletDirection;
    }

    public void InitializeBulletSpeed(float bulletSpeed)
    {
        _bulletSpeed = bulletSpeed;
    }

    private void MoveBullet()
    {
        transform.Translate(_bulletDirection * _bulletSpeed * Time.deltaTime);
    }

    private IEnumerator LifetimeCountDown()
    {
        float delay = 1;

        while (_currentLifetime < _bulletLifetime)
        {
            _waitForSeconds = new(delay);
            yield return _waitForSeconds;
            _currentLifetime++;
        }

        BulletLifetimeOver?.Invoke(this);
    }
}