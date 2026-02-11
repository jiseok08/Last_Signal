using Unity.VisualScripting;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    [SerializeField] int baseDamage = 15;
    [SerializeField] Transform turretHead;
    [SerializeField] Turret turret;

    [SerializeField] Vector3 dir;

    float timer;

    private void Awake()
    {
        turret = GetComponentInParent<Turret>();
        speed = turret.bulletSpeed; 
    }

    public void Init(Vector3 direction)
    {
        dir = direction.normalized;
        timer = 0f;
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Unit target = other.GetComponent<Unit>();

        if (target != null)
        {
            target.TakeDamage((int)((baseDamage + (turret.atk * 3)) * turret.atkMultiplier));

            Destroy(gameObject);
        }

        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            return;
        }

        Destroy(gameObject);
    }
}
