using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class SeekCoverBehavior : Node
{
    private NavMeshAgent agent;
    private Transform[] destinations;
    private float margin;

    private bool init = true;
    private float waitTime;
    private float waitTimer;
    private Transform destination;


    public SeekCoverBehavior(NavMeshAgent _agent, Transform[] _destinations, float _margin)
    {
        agent = _agent;
        destinations = _destinations;
        margin = _margin;

        // Pick a random wake up time between 0.25 and 0.5 seconds
        waitTime = Random.Range(0.25f, 0.5f);
    }

    public override NodeState Evaluate()
    {
        Debug.Log("SeekCoverBehavior");

        // Initialization, only happens on the first frame
        if (init)
        {
            // Wait a random small amount when entering the state
            destination = AIManager.Instance.ChooseFreeDestination(destinations);
            waitTime = Random.Range(0.25f, 0.5f);
            waitTimer = waitTime;
            init = false;
        }
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else if (Vector3.Distance(agent.transform.position, destination.position) > margin)
        {
            agent.ResetPath();
            agent.SetDestination(destination.position);
        }
        else if (AIManager.Instance.point.capturePercentRange >= 0.0f) // capture point is not under control
        {
            // Only exit the behavior if player gains an advantage
            agent.transform.parent = null;
            return NodeState.SUCCESS;
        }
        else
        {
            Debug.Log("Moving around cover");
            destination = AIManager.Instance.ChooseFreeDestination(destinations);
            waitTime = Random.Range(1f, 5f); // Stay in cover for 1 to 5 seconds
            waitTimer = waitTime;
            agent.transform.parent = null;
            agent.ResetPath();
            agent.SetDestination(destination.position);
        }

        return NodeState.RUNNING;
    }
}
