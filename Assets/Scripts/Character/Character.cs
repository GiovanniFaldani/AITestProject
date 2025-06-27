using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float turnSpeed;

    // input variables
    protected float x, z, fire;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Mathf.Clamp(hp, 0.0f, maxHp);
        if (hp <= 0) Die();
    }

    public virtual void Die()
    {

    }
}
