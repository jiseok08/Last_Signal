using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int baseDamage = 15;
    [SerializeField] Transform turretHead;
    [SerializeField] Turret turret;
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 dir;

    private void Awake()
    {
        turret = GetComponentInParent<Turret>();
        speed = turret.bulletSpeed; 
    }

    public void Init(Vector3 targetPos)
    {
        dir = (targetPos - transform.position).normalized;

        // 총알 모델이 forward(+Z) 방향으로 날아가게 되어있다는 기준
        transform.rotation = Quaternion.LookRotation(dir);

        rb.linearVelocity = dir * speed;   // Unity 6 계열이면 linearVelocity
        // rb.velocity = dir * speed;       // 구버전이면 velocity
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
