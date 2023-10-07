using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] float speed;
    [SerializeField] float MaxLifetime;
    [SerializeField] float _currentLifeTime;
    public static Bullet instanceHit
    {
        get;
        private set;
    }
    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
        _currentLifeTime -= Time.deltaTime;
        if (_currentLifeTime <= 0)
        {
            FactoryPool.Instance.ComeBackBullet(this);
        }
    }
    public static void TurnOn(Bullet b)
    {
        b.Reset();
        b.gameObject.SetActive(true);
    }
    public static void Turnoff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }
    private void Reset()
    {
        _currentLifeTime = MaxLifetime;
    }
}
