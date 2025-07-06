using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class SeekHealthPackBehavior : Node
{
    private NavMeshAgent agent;
    private Transform[] destinations;
    private float margin;
    private AIController aiController;

    private bool init = true;
    private float waitTime;
    private float waitTimer;
    private Transform destination;


    public SeekHealthPackBehavior(NavMeshAgent _agent, Transform[] _destinations, float _margin, AIController _aiController)
    {
        agent = _agent;
        destinations = _destinations;
        margin = _margin;
        aiController = _aiController;

        // Pick a random wake up time between 0.25 and 0.5 seconds
        waitTime = Random.Range(0.25f, 0.5f);
    }

    public override NodeState Evaluate()
    {
        if (aiController.GetCurrentHp() > 1) return NodeState.SUCCESS; // exit behaviour if health is high enough

        Debug.Log("SeekHealthPackBehavior");

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
            agent.SetDestination(destination.position);
            agent.transform.parent = destination;
        }
        else
        {
            Debug.Log("Moving to health pack");
            destination = AIManager.Instance.ChooseFreeDestination(destinations);
            agent.transform.parent = null;
            agent.SetDestination(destination.position);
        }

        return NodeState.RUNNING;
    }
}
