using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float turnSpeed;
    [SerializeField] protected Vector3 lookDirection;
    [SerializeField] protected float shotCooldown;
    [SerializeField] protected float respawnCooldown;
    [SerializeField] protected int damage;
    [SerializeField] protected GameObject shotPrefab;
    [SerializeField] protected Vector3 spawnPoint;

    // input variables
    protected float x, z, fire;

    // timers
    protected float shotTimer = 0.0f;

    // components
    protected Rigidbody rb;


    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Mathf.Clamp(hp, 0.0f, maxHp);
        if (hp <= 0) Respawn();
    }

    public void Heal(int healing)
    {
        hp += healing;
        hp = (int)Mathf.Clamp(hp, 0.0f, maxHp);
    }

    protected void Respawn()
    {
       StartCoroutine(WaitForRespawn());
    }

    protected IEnumerator WaitForRespawn()
    {
        transform.position = spawnPoint;
        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(respawnCooldown);
        gameObject.SetActive(true);
    }

    protected void TurnTowardsLookDirection()
    {

    }

    public void ApplyMovement()
    {
        float xSpeed = x * speed * Time.deltaTime;
        float zSpeed = z * speed * Time.deltaTime;
        xSpeed = Mathf.Clamp(xSpeed, -maxSpeed, maxSpeed);
        zSpeed = Mathf.Clamp(zSpeed, -maxSpeed, maxSpeed);
        rb.AddForce(new Vector3(xSpeed, 0, zSpeed), ForceMode.VelocityChange);
    }

    public void CheckForShot()
    {
        shotTimer -= Time.deltaTime;
        shotTimer = Mathf.Clamp(shotTimer, 0.0f, shotCooldown);
        if(shotTimer <= 0.0f && fire > 0)
        {
            Shoot();
            shotTimer = shotCooldown;
        }
    }

    public void Shoot()
    {

    }
}
