using System.Collections;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int healingAmount;
    [SerializeField] private float respawnTime;

    public bool isActive = true;
    private Character charComponent;
    private Collider col;
    private MeshRenderer m_rend;

    private void Awake()
    {
        col = GetComponent<Collider>();
        m_rend = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            charComponent = other.GetComponent<Player>();
            charComponent.Heal(healingAmount);
            StartCoroutine(WaitForRespawn());
        }
        else if (other.CompareTag("Enemy"))
        {
            charComponent = other.GetComponent<AIController>();
            charComponent.Heal(healingAmount);
            StartCoroutine(WaitForRespawn());
        }
    }

    private void DeactivatePack()
    {
        isActive = false;
        col.enabled = false;
        m_rend.enabled = false;
    }

    private void ActivatePack()
    {
        isActive = true;
        col.enabled = true;
        m_rend.enabled = true;
    }

    private IEnumerator WaitForRespawn()
    {
        DeactivatePack();
        yield return new WaitForSeconds(respawnTime);
        ActivatePack();
    }
}
