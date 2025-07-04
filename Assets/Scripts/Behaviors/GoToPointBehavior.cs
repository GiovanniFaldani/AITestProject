using UnityEngine;
using UnityEngine.AI;
using BehaviourTree;

public class GoToPointBehavior : Node
{
    private NavMeshAgent agent;
    private Transform[] destinations;
    private float margin;

    private bool waiting = true;
    private float waitTime;
    private float waitTimer;
    private Transform destination;


    public GoToPointBehavior(NavMeshAgent _agent, Transform[] _destinations, float _margin)
    {
        agent = _agent;
        destinations = _destinations;
        margin = _margin;

        // Pick a random wake up time between 0.25 and 0.5 seconds
        waitTime = Random.Range(0.25f, 0.5f);
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = false;

        // Initialization , only happens on the first pass
        if (waiting)
        {
            destination = AIManager.Instance.ChooseFreeDestination(destinations);
            waitTime = Random.Range(0.25f, 0.5f);
            waitTimer = waitTime;
            waiting = false;
        }
        if(waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
        }
        else if (Vector3.Distance(agent.transform.position, destination.position) > margin)
        {
            agent.SetDestination(destination.position);
            agent.transform.parent = destination;
        }
        else
        {
            agent.transform.parent = null;
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
