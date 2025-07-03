using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private ShotSource source;
    
    private float lifeTimer;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void Start()
    {
        lifeTimer = lifeTime;
    }


    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer < 0)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (source)
        {
            case ShotSource.Player:
                if (other.CompareTag("Player")) return;
                if (other.CompareTag("Enemy")) other.GetComponent<AIController>().TakeDamage(damage);
                Destroy(gameObject);
                break;
            case ShotSource.Enemy:
                if (other.CompareTag("Enemy")) return;
                if (other.CompareTag("Player")) other.GetComponent<Player>().TakeDamage(damage);
                Destroy(gameObject);
                break;
            default:
                break;
        }
        
    }
}
