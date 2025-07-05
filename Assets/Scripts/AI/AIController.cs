using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(NavMeshAgent))]
public class AIController : Character
{
    [SerializeField] public float activationRange;
    [SerializeField] public float destinationMargin = 0.1f;

    public NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = this.speed;
    }

    protected override void Update()
    {
        base.Update();
    }

}
