using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ObjectPool <T>
{
    Func<T> BulletFactory;
    List<T> _ActualAmountBullets;
    bool IsDinamyc;
    Action<T> OnBullet;
    Action<T> OffBullet;
    public ObjectPool(Func<T> factoryBullets, Action<T> On, Action<T> Off, int Amount, bool isDynamic = true)
    {
        BulletFactory = factoryBullets;
        OnBullet = On;
        OffBullet = Off;
        IsDinamyc = isDynamic;
        _ActualAmountBullets = new List<T>(Amount);
        for (int i = 0; i < Amount; i++)
        {
            T bullet = BulletFactory();
            OffBullet(bullet);
            _ActualAmountBullets.Add(bullet);
        }
    }

    public T GiveBullet()
    {
        var result = default(T);
        if (_ActualAmountBullets.Count > 0)
        {
            result = _ActualAmountBullets[0];
            _ActualAmountBullets.RemoveAt(0);
        }
        else if (IsDinamyc)
        {
            result = BulletFactory();
        }
        OnBullet(result);
        return result;
    }

    public void ReturnBullet(T Obj)
    {
        OffBullet(Obj);
        _ActualAmountBullets.Add(Obj);
    }
}
