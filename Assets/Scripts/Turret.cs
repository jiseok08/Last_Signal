using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float atkMultiplier;
    [SerializeField] public int level;
    [SerializeField] public int range;
    [SerializeField] public int atk;
    [SerializeField] private float atkCooldown;

    [SerializeField] public List<Unit> targetList;
    [SerializeField] Transform bulletPoint;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform muzzle;


    protected void Attack()
    {
        Instantiate(Resources.Load("Bullet"), bulletPoint);
    }

    public void Fire(Transform target)
    {
        Bullet b = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
        Vector3 direction = (target.position - muzzle.position);
        b.Init(direction);
    }
}
