using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int atk;
    [SerializeField] protected int hp;
    [SerializeField] protected float speed;
    [SerializeField] protected float rotateSpeed = 720f;
    [SerializeField] public Unit ParentPrefab;
    [SerializeField] protected Collider attackRange;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Core core;
    [SerializeField] protected Player player;
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

    }

    private void Start()
    {
        // Invoke(nameof(Die), 1);
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
    }

    void FixedUpdate()
    {
        Move();
    }

    private void OnTriggerEnter(Collider other)
    {
        Core core = other.GetComponent<Core>();

        if (core != null)
        {
            core.TakeDamage(100);

            Die();
        }
    }
}
