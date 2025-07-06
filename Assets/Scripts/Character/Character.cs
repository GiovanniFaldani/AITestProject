using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected int maxHp;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float captureSpeed;
    [SerializeField] protected int damage;
    [SerializeField] protected float shotCooldown;
    [SerializeField] protected float respawnCooldown;
    [SerializeField] protected GameObject shotPrefab;
    [SerializeField] protected GameObject meshNode;
    [SerializeField] protected Transform shotSocket;
    [SerializeField] protected Vector3 spawnPoint;
    [SerializeField] protected bool isActive = true;

    // input variables
    protected float x, z, fire;
    private int baseHp;
    [SerializeField] protected Vector3 lookDirection;

    // timers
    protected float shotTimer = 0.0f;

    // components
    protected Rigidbody rb;
    private Collider col;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        baseHp = hp;
    }

    protected virtual void Update()
    {
        if (!isActive) return;
    }

    public int GetCurrentHp()
    {
        return hp;
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
        hp = baseHp;
        StartCoroutine(WaitForRespawn());
    }

    protected IEnumerator WaitForRespawn()
    {
        transform.position = spawnPoint;
        DeactivateCharacter();
        yield return new WaitForSeconds(respawnCooldown);
        ActivateCharacter();
    }

    protected void TurnTowardsLookDirection()
    {
        transform.rotation = Quaternion.LookRotation(lookDirection);
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
        Projectile projectile = Instantiate(shotPrefab, shotSocket).GetComponent<Projectile>();
        projectile.transform.parent = null;
        projectile.SetDamage(damage);
    }

    public float GetCaptureSpeed()
    {
        return captureSpeed;
    }

    protected void DeactivateCharacter()
    {
        rb.linearVelocity = Vector3.zero;
        isActive = false;
        col.enabled = false;
        meshNode.SetActive(false);
    }

    protected void ActivateCharacter()
    {
        isActive = true;
        col.enabled = true;
        meshNode.SetActive(true);
    }
}
