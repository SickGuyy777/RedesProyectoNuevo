using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPool : MonoBehaviour
{
    public static FactoryPool Instance { get; private set; }
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] int bulletStock = 5;
    ObjectPool<Bullet> pool;

    void Start()
    {
        Instance = this;
        pool = new ObjectPool<Bullet>(CreateBullet, Bullet.TurnOn, Bullet.Turnoff, bulletStock);
    }

    Bullet CreateBullet()
    {
        return Instantiate(bulletPrefab, transform);
    }

    public Bullet GetObj()
    {
        return pool.GiveBullet();
    }

    public void ComeBackBullet(Bullet Bullet)
    {
        pool.ReturnBullet(Bullet);
    }
}
