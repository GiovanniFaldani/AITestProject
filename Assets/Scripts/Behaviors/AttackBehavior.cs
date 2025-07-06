using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class AttackBehavior : Node
{
    private AIController aiController;


    public AttackBehavior(AIController _aiController)
    {
        aiController = _aiController;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("AttackBehavior");

        if (aiController.CanAttackPlayer())
        {
            Debug.Log("Attacking Player");
            aiController.CheckForShot();
        }
        else
        {
            return NodeState.FAILURE;
        }

        return NodeState.RUNNING;
    }
}
