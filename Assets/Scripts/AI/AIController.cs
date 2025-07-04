using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class AIController : Character
{
    [SerializeField] public float activationRange;
    [SerializeField] public float destinationMargin = 0.1f;

    public NavMeshAgent agent;
    private EnemyBT enemyBT;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        enemyBT = GetComponent<EnemyBT>();
        agent.speed = this.speed;
    }

}
