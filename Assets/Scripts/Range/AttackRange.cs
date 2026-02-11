using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] Unit unit;

    private void Awake()
    {
        unit = transform.parent.GetComponent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player target = other.GetComponent<Player>();

        if (target != null)
        {
            unit.Attack(target);
        }

        // 코어일 때 상황
    }
}
