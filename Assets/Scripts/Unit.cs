using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int atk;
    [SerializeField] protected int hp;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotateSpeed;
    [SerializeField] protected Collider attackRange;
    [SerializeField] public Transform target;

    private void Awake()
    {
        
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public abstract void Attack(Damageable other);

    public void Move()
    {
        
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Move();
    }
}
