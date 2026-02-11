using UnityEngine;

public class Speedtype : Unit
{


    void Start()
    {
        atk = 5;
        hp = 30;
        speed = 30;
    }

    public override void Attack(Damageable other)
    {
        other.TakeDamage(atk); // 애니메이션 이밴트로 바꾸기
    }
}
