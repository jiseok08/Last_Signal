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

    protected void Attack()
    {
        Instantiate(Resources.Load("Bullet"), bulletPoint);
    }
}
