using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class GetInRangeBehavior : Node
{
    private NavMeshAgent agent;
    private AIController aiController;

    public GetInRangeBehavior(NavMeshAgent _agent, AIController _aiController)
    {
        agent = _agent;
        aiController = _aiController;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("GetInRangeBehavior");

        if (aiController.CanAttackPlayer())
        {
            Debug.Log("Player in attack range");
            return NodeState.SUCCESS;
        }
        else if (aiController.CanSeePlayer())
        {
            Debug.Log("Chasing player");
            agent.ResetPath();
            agent.SetDestination(aiController.player.transform.position);
        }
        else
        {
            return NodeState.FAILURE;
        }

        return NodeState.RUNNING;
    }

}