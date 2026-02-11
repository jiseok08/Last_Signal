using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Damageable
{
    [SerializeField] int hp = 100;
    [SerializeField] float speed = 5f;
    [SerializeField] Transform cam;
    [SerializeField] Transform pickupRange;

    Rigidbody rb;
    Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (cam == null && Camera.main != null)
            cam = Camera.main.transform;

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); 
    }

    void FixedUpdate()
    {
        Vector3 dir;

        if (cam == null)
        {
            dir = new Vector3(moveInput.x, 0f, moveInput.y);
        }
        else
        {
            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            dir = right * moveInput.x + forward * moveInput.y;
        }

        if (dir.sqrMagnitude > 1f) dir.Normalize();

        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage!");

        hp -= damage;

        if (hp <= 0)
        {
            State.Publish(Condition.FINISH);
        }
    }
}
