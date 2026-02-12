using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float rotateSpeed = 720f;
    [SerializeField] protected int atk;
    [SerializeField] protected int hp;
    [SerializeField] public Unit ParentPrefab;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public Core core;
    [SerializeField] public Player player;
    [SerializeField] public Transform target;




    [SerializeField] bool rotateToMoveDir = true;

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        core = GameManager.Instance.Core;
        player = GameManager.Instance.player;

        target = core.transform;

        rotateSpeed = 720f;
    }

    private void OnEnable()
    {
        hp = (int)(hp * (1 + (GameManager.Instance.Wave.wave - 1) * 0.12f));
        speed = speed * (1 + (GameManager.Instance.Wave.wave - 1) * 0.03f);
    }

    public void ChangeTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void Move()
    {
        if (target == null) return;

        Vector3 to = target.position - rb.position;
        to.y = 0f;

        if (to.sqrMagnitude < 0.0001f) return;

        Vector3 dir = to.normalized;
        Vector3 next = rb.position + dir * speed * Time.fixedDeltaTime;
        rb.MovePosition(next);

        if (rotateToMoveDir)
        {
            Quaternion goal = Quaternion.LookRotation(dir, Vector3.up);
            Quaternion rot = Quaternion.RotateTowards(rb.rotation, goal, rotateSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rot);
        }
    }

    public void Attack(Damageable damageable)
    {
        Debug.Log("Attack");

        damageable.TakeDamage(15 + (atk * 3));

        Die();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.Spawn.Release(ParentPrefab, this);

        ChangeTarget(core.transform);
    }

    void FixedUpdate()
    {
        Move();
    }
}
