using UnityEngine;

public class UnitRange : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] Collider target;

    private void Awake()
    {
        unit = transform.parent.GetComponent<Unit>();
    
        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();

        target = other;

        if (damageable != null)
        {
            unit.ChangeTarget(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == target)
        {
            unit.ChangeTarget(unit.target);
        }
    }
}
