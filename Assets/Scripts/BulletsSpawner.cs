using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletsSpawner : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _shootingDelay;
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _maximumPoolSize;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _weaponBarrelPosition;
    [SerializeField] private Transform _weaponBasePosition;

    private ObjectPool<Bullet> _bulletsPool;

    private void Awake()
    {
        _bulletsPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_bullet),
            actionOnGet: (bullet) => AccompanyGetObject(bullet),
            actionOnRelease: (bullet) => AccompanyReleaseObject(bullet),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _maximumPoolSize);
    }

    private void Start()
    {
        StartCoroutine(bulletSpawn());
    }

    private void AccompanyGetObject(Bullet bullet)
    {
        SetBulletPosition(bullet);
        bullet.InitializeBulletDirection(GetBulletDirection());
        bullet.InitializeBulletSpeed(_bulletSpeed);
        bullet.InitializeLifetimeValue(_bulletLifetime);

        bullet.gameObject.SetActive(true);
        bullet.BulletLifetimeOver += OnBulletLifetimeOver;
    }

    private void AccompanyReleaseObject(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.BulletLifetimeOver -= OnBulletLifetimeOver;
    }

    private void SetBulletPosition(Bullet bullet)
    {
        Vector3 defaultBulletPosition = _weaponBarrelPosition.position;
        bullet.transform.position = defaultBulletPosition;
    }

    private Vector3 GetBulletDirection()
    {
        Vector3 bulletDirection = (_weaponBarrelPosition.position - _weaponBasePosition.position).normalized;
        return bulletDirection;
    }

    private IEnumerator bulletSpawn()
    {
        WaitForSeconds shootingDelaying = new(_shootingDelay);

        while (true)
        {
            _bulletsPool.Get();
            yield return shootingDelaying;
        }
    }

    private void OnBulletLifetimeOver(Bullet bullet)
    {
        _bulletsPool.Release(bullet);
    }
}