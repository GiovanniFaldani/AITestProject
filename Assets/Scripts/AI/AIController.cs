using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class AIController : Character
{
    [SerializeField] public float activationRange;
    [SerializeField] public float attackRange;
    [SerializeField] public float destinationMargin = 0.1f;

    public NavMeshAgent agent;
    public EnemyBT enemyBT;
    public Player player;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        enemyBT = GetComponent<EnemyBT>();
        player = FindAnyObjectByType<Player>();
        agent.speed = this.speed;
        fire = 1;
    }

    protected override void Update()
    {
        base.Update();
        agent.isStopped = !isActive;
    }

    public bool CanSeePlayer()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < activationRange)
        {
            RaycastHit hit;
            Vector3 direction = player.transform.position - agent.transform.position;
            if (Physics.Raycast(agent.transform.position, direction, out hit, activationRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    lookDirection = player.transform.position - transform.position;
                    TurnTowardsLookDirection();
                    return true;
                }
            }
        }
        return false;
    }
    public bool CanAttackPlayer()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < attackRange)
        {
            RaycastHit hit;
            Vector3 direction = player.transform.position - agent.transform.position;
            if (Physics.Raycast(agent.transform.position, direction, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    lookDirection = player.transform.position - transform.position;
                    TurnTowardsLookDirection();
                    return true;
                }
            }
        }
        return false;
    }
}
